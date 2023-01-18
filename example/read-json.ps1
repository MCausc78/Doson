param(
	[string] $Config,
	[string] $Filename = "test.bin"
)

try {
	Add-Type -Assembly "bin\$Config\net6.0\Doson.dll"
} catch {
	$_
	Exit 1
}

try {
	$file = [System.IO.File]::Open($filename, [System.IO.FileMode]::Open)
} catch {
	$_
	Exit 2
}

try {
	$reader = New-Object System.IO.BinaryReader($file, [System.Text.Encoding]::UTF8, $false)
} catch {
	$_
	Exit 3
}

$obj = [Doson.Utils.BinaryUtil]::ReadFrom($reader)
if ($obj -ne $null) {
	Write-Host $obj.Build()
} else {
	Write-Host null
}

$reader.Close()
$reader.Dispose()
$file.Close()
$file.Dispose()
