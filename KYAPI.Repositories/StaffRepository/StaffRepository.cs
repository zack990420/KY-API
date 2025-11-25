using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KYAPI.Data;
using KYAPI.DTOs;
using KYAPI.Entities;
using KYAPI.Services;

namespace KYAPI.Repositories;

public class StaffRepository : BaseRepository<StaffInfo>, IStaffRepository
{
    private readonly KYAPI.Services.IIdHasher _idHasher;
    private readonly UserManager<ApplicationUser> _userManager;

    public StaffRepository(AppDbContext context, IIdHasher idHasher, UserManager<ApplicationUser> userManager) : base(context)
    {
        _idHasher = idHasher;
        _userManager = userManager;
    }

    public async Task<GlobalApiResponse<string>> CreateStaffAccount(CreateStaffRequestDto request, long createdBy)
    {
        // Start transaction to ensure all data is created atomically
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. Check if user already exists
            var existingUser = await _userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                return new GlobalApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "User with this username already exists",
                    Value = null
                };
            }

            // 2. Create ApplicationUser
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                EmailConfirmed = true, // Auto-confirm for staff
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);

            if (!createUserResult.Succeeded)
            {
                var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
                return new GlobalApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = $"Failed to create user: {errors}",
                    Value = null
                };
            }

            // 3. Assign role (default to "Staff" if not specified)
            var roleResult = await _userManager.AddToRoleAsync(user, "Staff");
            if (!roleResult.Succeeded)
            {
                await transaction.RollbackAsync();
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return new GlobalApiResponse<string>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = $"Failed to assign role: {errors}",
                    Value = null
                };
            }

            // 4. Create StaffInfo
            var staffInfo = new StaffInfo
            {
                StaffUserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                NickName = request.NickName,
                Gender = request.Gender,
                Nationality = request.Nationality,
                IdentificationNo = request.IdentificationNo,
                PassportNo = request.PassportNo,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                EmploymentType = request.EmploymentType,
                EmploymentStatus = request.EmploymentStatus,
                Position = request.Position,
                Department = request.Department,
                DateJoined = request.DateJoined ?? DateTime.UtcNow,
                EntryBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            await _context.StaffInfo.AddAsync(staffInfo);

            // 5. Create StaffAddressProfile
            var addressProfile = new StaffAddressProfile
            {
                StaffUserId = user.Id,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                EntryBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            await _context.StaffAddressProfile.AddAsync(addressProfile);

            // 6. Create StaffPayrollInfo
            var payrollInfo = new StaffPayrollInfo
            {
                StaffUserId = user.Id,
                Salary = request.Salary,
                BankName = request.BankName,
                BankAccountNumber = request.BankAccountNumber,
                EPFNo = request.EPFNo,
                SocsoNo = request.SocsoNo,
                TaxNo = request.TaxNo,
                EntryBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            await _context.StaffPayrollInfo.AddAsync(payrollInfo);

            // 7. Create StaffEmergencyContact
            var emergencyContact = new StaffEmergencyContact
            {
                StaffUserId = user.Id,
                Name = request.EmergencyContactName,
                Relationship = request.EmergencyContactRelationship,
                PhoneNumber = request.EmergencyContactPhoneNumber,
                Email = request.EmergencyContactEmail,
                EntryBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            await _context.StaffEmergencyContact.AddAsync(emergencyContact);

            // 8. Save all changes
            await _context.SaveChangesAsync();

            // 9. Commit transaction
            await transaction.CommitAsync();

            return new GlobalApiResponse<string>
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Staff account created successfully",
                Value = _idHasher.Hash(user.Id) // Return hashed user ID
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new GlobalApiResponse<string>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = $"An error occurred while creating staff account: {ex.Message}",
                Value = null
            };
        }
    }
}