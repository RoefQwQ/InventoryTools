# 批量删除失败的工作流运行记录
$repo = "RoefQwQ/InventoryTools"
$batch = 1

while ($true) {
    Write-Host "Fetching failed runs (batch $batch)..."
    $runsJson = gh run list --repo $repo --status failure -L 100 --json databaseId,displayTitle,name 2>$null
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($runsJson)) {
        Write-Host "No more failed runs or error occurred."
        break
    }

    $runs = $runsJson | ConvertFrom-Json
    if ($runs.Count -eq 0) {
        Write-Host "No more failed runs to delete."
        break
    }

    Write-Host "Found $($runs.Count) failed runs. Deleting..."

    foreach ($run in $runs) {
        $id = $run.databaseId
        $title = $run.displayTitle
        $name = $run.name
        Write-Host "  Deleting run $id - $name ($title)..." -NoNewline
        $result = gh run delete $id --repo $repo 2>&1
        if ($LASTEXITCODE -eq 0) {
            Write-Host " OK" -ForegroundColor Green
        } else {
            Write-Host " FAILED: $result" -ForegroundColor Red
        }
    }

    $batch++
}

Write-Host "All done!"
