$ErrorActionPreference = "Stop"
[console]::OutputEncoding = [Text.Encoding]::Utf8

Function Set-HardLink {
    [CmdletBinding()]
    param (
        [string]$existingFileAbsolutePath,
        [string]$newFileAbsolutePath
    )

    if (-Not (Test-Path $existingFileAbsolutePath -Type 'Leaf')) {
        Write-Error "'$existingFileAbsolutePath' doesn't exists"
        throw "'$existingFileAbsolutePath' doesn't exists"
    }

    if (Test-Path $newFileAbsolutePath -Type 'Leaf') {

        $backupFileAbsolutePath = "$newFileAbsolutePath.backup.$(get-date -UFormat '%F-%H-%M-%S')"
        Write-Information "Found existing file '$newFileAbsolutePath' that was marked as target for hardlink" -InformationAction Continue
        Write-Information "Creating backup: '$backupFileAbsolutePath'" -InformationAction Continue

        Move-Item -Path $newFileAbsolutePath -Destination $backupFileAbsolutePath
    }

    fsutil hardlink create $newFileAbsolutePath $existingFileAbsolutePath
}

Export-ModuleMember -Function 'Set-HardLink'