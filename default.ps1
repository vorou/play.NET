Properties {
    $solution_dir = ".\Src"
    $verbosity = "minimal"
    $test_runner = "$solution_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
}

Task default -Depends Compile

Task Compile {
    Exec { msbuild /verbosity:$verbosity "$solution_dir\playNET.sln" }
}

Task RunTests -Depends Compile {
    Call "$test_runner" "$solution_dir\playNET.Tests\bin\Debug\playNET.Tests.dll"
}
