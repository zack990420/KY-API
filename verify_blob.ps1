$baseUrl = "http://localhost:5086"
$filePath = "test-image.txt"
Set-Content -Path $filePath -Value "This is a test file content for blob storage."

# 1. Upload File
Write-Host "Uploading file..."
try {
    $uri = "$baseUrl/api/TestBlob/upload"
    $fileBytes = [System.IO.File]::ReadAllBytes($filePath)
    $boundary = "---------------------------" + [DateTime]::Now.Ticks.ToString("x")
    $LF = "`r`n"

    $bodyLines = (
        "--$boundary",
        "Content-Disposition: form-data; name=`"file`"; filename=`"$filePath`"",
        "Content-Type: text/plain",
        "",
        [System.Text.Encoding]::ASCII.GetString($fileBytes),
        "--$boundary--"
    ) -join $LF

    $response = Invoke-RestMethod -Uri $uri -Method Post -Body $bodyLines -ContentType "multipart/form-data; boundary=$boundary"
    Write-Host "Upload Successful. URL: $($response.url)"
} catch {
    Write-Error "Upload failed: $_"
    exit 1
} finally {
    Remove-Item -Path $filePath -ErrorAction SilentlyContinue
}
