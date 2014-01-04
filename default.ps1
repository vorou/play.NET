Properties {
    $verbosity = "minimal"
    $sln_dir = ".\Src"
    $msbuild = "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"
    $test_runner = "$sln_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
    $sln = "$sln_dir\playNET.sln"
    $test_dll = "$sln_dir\playNET.Tests\bin\Debug\playNET.Tests.dll" 
}

Task default -Depends build

Task build -Depends compile, tests

Task compile {
    Exec { & $msbuild /v:$verbosity $sln }
}

Task tests -Depends compile {
    Exec { & $test_runner $test_dll }
}
