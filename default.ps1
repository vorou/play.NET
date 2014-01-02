Properties {
    $solution_dir = ".\Src"
    $test_runner = "$solution_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
    $msbuild = "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"
    $verbosity = "minimal"
}

Task default -Depends build

Task build -Depends compile, tests

Task compile {
    Exec { & "$msbuild" /v:$verbosity "$solution_dir\playNET.sln" }
}

Task tests -Depends compile {
    Exec { & "$test_runner" "$solution_dir\playNET.Tests\bin\Debug\playNET.Tests.dll" }
}

Task play -Depends build {
    Copy .\Audio\vot-tak-vot.mp3 "$env:TEMP"
    $service = Start-Process .\Src\playNET.Service\bin\Debug\playNET.Service.exe -PassThru
    Invoke-WebRequest http://localhost:666/play -Method POST
    Start-Sleep 2
    Stop-Process $service
}
