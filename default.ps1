Properties {
    $solution_dir = ".\Src"
    $test_runner = "$solution_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
    $verbosity = "minimal"
}

Task default -Depends build

Task build -Depends compile, tests

Task compile {
    Exec { msbuild /v:$verbosity "$solution_dir\playNET.sln" }
}

Task tests -Depends compile {
    Exec { & "$test_runner" "$solution_dir\playNET.Tests\bin\Debug\playNET.Tests.dll" }
}
