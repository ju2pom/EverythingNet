param($installPath, $toolsPath, $package, $project)
Write-Host $installPath
$projectFullName = $project.FullName


ForEach ($c in $project.ConfigurationManager)
{
	$projectPath = ($project.Properties | Where Name -match FullPath).Value
	$dest = $projectPath + ($c.Properties | Where Name -match OutputPath).Value
	$src = "$($installPath)/content"
	robocopy $src $dest /XO
}

$project.Properties.Item("PostBuildEvent").Value = "xcopy `"$($src)`" `"$($dest)`" /Y" 


# Write-Host 
#ForEach ($p in $project.Properties)
#{
#	Write-Host $p.Name + " " + $p.Value
#}
