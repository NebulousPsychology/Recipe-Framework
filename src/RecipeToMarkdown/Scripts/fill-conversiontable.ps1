<#
.SYNOPSIS
    fills the lower-left of a unit conversion matrix by taking inverses of the upper-right
#>
[cmdletbinding()]param(
    [ValidateScript({ if (-not $_.Exists){throw "file '$_' not found"}})]
    [io.FileInfo]$csvFile="$PSScriptRoot\..\Data\Conversions.wet.csv",

    [io.DirectoryInfo]$OutputDirectory=$csvFile.Directory
    )

    if (-not $OutputDirectory.Exists){
        $OutputDirectory = New-Item -ItemType Directory $OutputDirectory -Force -Confirm
    }

    $before = Import-Csv $csvFile
    $after = Import-Csv $csvFile

$units = $after.fromunit
foreach($cvrow in $after){
    $cvrow|Format-Table|Out-String|Write-Debug
    $fromunit= $cvrow.FromUnit
    foreach ($tounit in $units){
        Write-Debug " $fromUnit > $toUnit " -Verbose
        if ([string]::IsNullOrWhiteSpace($cvrow."$tounit") ){
            $inverse = 1.0/($after|Where-Object fromunit -eq $tounit|Select-Object -f 1 -exp $fromunit)
            Write-Debug "need-calculate: $fromunit > $tounit = $inverse"

            $cvrow."$tounit" = [double][math]::Round($inverse, 7)
        }else {
            Write-Debug "$fromunit > $toUnit = $($cvrow."$toUnit")"
            $cvrow."$tounit"=[double]$cvrow."$tounit"
        }
    }
}
Write-Verbose "Before $($before|Format-Table|Out-String)"
Write-Verbose " After $($after|Format-Table|Out-String)"

$after|Export-Csv (join-path $OutputDirectory "$($csvfile.basename).full.csv")

$fanout = foreach ($from in $after.fromunit){
    foreach ($to in $after.fromunit){
        [PSCustomObject]@{
            From = $from
            To = $to
            Constant = [double]($after|Where-Object FromUnit -eq $from | Select-Object -exp $to -First 1)
        }
    }
}
#$fanout | ConvertTo-Json -Compress
#$fanout|sort from|group from | select -ExcludeProperty values,Count |ctj
