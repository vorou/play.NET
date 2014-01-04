Properties {
    $verbosity = "minimal"
    $sln_dir = ".\Src"
    $sln = "$sln_dir\playNET.sln"
    $msbuild = "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"
    $test_runner = "$sln_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
    $test_proj = "$sln_dir\playNET.Tests\playNET.Tests.csproj" 
    $test_dll = "$sln_dir\playNET.Tests\bin\Debug\playNET.Tests.dll" 
}

Task default -Depends build

Task build -Depends compile_sln, run_tests

Task compile_sln {
    Exec { & $msbuild /v:$verbosity $sln }
}

Task compile_tests {
    Exec { & $msbuild /v:$verbosity $test_proj }
}

Task run_tests -Depends compile_tests {
    Exec { & $test_runner $test_dll }
}
