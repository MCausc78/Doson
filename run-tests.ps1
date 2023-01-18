#!/usr/bin/env powershell

param(
	[string]
	$Config
)

try {
	Add-Type -Assembly "bin\$Config\net6.0\Doson.dll"
} catch {
	$_
	Exit 1
}

function Test-1 {
	return (New-Object Doson.JsonInteger(625)).Build()
}

function Test-2 {
	return (New-Object Doson.JsonFloat(3.14)).Build()
}

function Test-3 {
	return (New-Object Doson.JsonCharacter([char] 'C')).Build()
}

function Test-4 {
	return (New-Object Doson.JsonString("Hello, world!")).Build()
}

function Test-5 {
	return (New-Object Doson.JsonBuilder).Build()
}

function Test-6 {
	return (New-Object Doson.JsonArray).Build()
}

function Test-7 {
	return (New-Object Doson.JsonBuilder).Put("pi", 3.14).Build()
}

function Test-8 {
	$fibonacciNumbers = New-Object Doson.JsonArray
	$x, $y = 1, 1
	for ($i = 0; $i -lt 36; ++$i) {
		$fibonacciNumbers.Append($x)
		$x, $y = $y, ($x + $y)
	}
	return $fibonacciNumbers.Build()
}

function Test-9 {
	$object = New-Object Doson.JsonBuilder
	$object.Put("myint", 625)
	$object.Put("myfloat", 3.14)
	$object.Put("mychar", [char] 'C')
	$object.Put("mystring", "Hello world!")
	$array = New-Object Doson.JsonArray
	1..5 | %{
		$array.Append($_)
	}
	return $object.Put("myarray", $array).Build()
}

function Test-10 {
	$array1 = New-Object Doson.JsonArray
	$k = 1
	for ($i = 0; $i -lt 3; ++$i) {
		$array2 = New-Object Doson.JsonArray
		for ($j = 0; $j -lt 3; ++$j) {
			$array2.Append($k++) | Out-Null
		}
		$array1.Append($array2) | Out-Null
	}
	return $array1.Build()
}

function Test-11 {
	return (New-Object Doson.JsonArray).Append((New-Object Doson.JsonArray).Append((New-Object Doson.JsonArray).Append((New-Object Doson.JsonArray)))).Build()
}

$tests = (	("Test-1", "625"),
			("Test-2", "3.14"),
			("Test-3", "'C'"),
			("Test-4", '"Hello, world!"'),
			("Test-5", '{}'),
			("Test-6", '[]'),
			("Test-7", '{"pi":3.14}'),
			("Test-8", '[1,1,2,3,5,8,13,21,34,55,89,144,233,377,610,987,1597,2584,4181,6765,10946,17711,28657,46368,75025,121393,196418,317811,514229,832040,1346269,2178309,3524578,5702887,9227465,14930352]'),
			("Test-9", '{"myint":625,"myfloat":3.14,"mychar":''C'',"mystring":"Hello world!","myarray":[1,2,3,4,5]}'),
			("Test-10", '[[1,2,3],[4,5,6],[7,8,9]]'),
			("Test-11", '[[[[]]]]'))

for ($i = 1; $i -le $tests.Length; ++$i) {
	$test = $tests[$i - 1];
	$result = $null
	try {
		$result = Invoke-Expression $test[0]
	} catch {
		$_
		Write-Host -ForegroundColor Red "Function of $i is missing, stopping`n"
		Exit 3
	}
	if ($result -eq $test[1]) {
		Write-Host -ForegroundColor Green "Test $i passed"
	} else {
		Write-Host -ForegroundColor Red (
			"Test $i failed:`n - Expected: `"{0}`"`n - Got: `"{1}`"" -f $test[1], $result
		)
		Exit 2
	}
}