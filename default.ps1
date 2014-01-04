properties {
    $verbosity = "minimal"
    $sln_dir = ".\Src"
    $sln = "$sln_dir\playNET.sln"
    $msbuild = "C:\Program Files (x86)\MSBuild\12.0\Bin\amd64\MSBuild.exe"
    $msbuild_settings = "/v:$verbosity"
    $test_runner = "$sln_dir\packages\Fixie.0.0.1.120\lib\net45\Fixie.Console.exe"
    $test_proj = "$sln_dir\playNET.Tests\playNET.Tests.csproj" 
    $test_dll = "$sln_dir\playNET.Tests\bin\Debug\playNET.Tests.dll" 
    $app_proj = "$sln_dir\playNET.App\playNET.App.csproj"
}

task default -depends build

task build -depends compile_sln, run_tests, package

task compile_sln {
    exec { & $msbuild $msbuild_settings $sln }
}

task compile_tests {
    exec { & $msbuild $msbuild_settings $test_proj }
}

task run_tests -depends compile_tests {
    exec { & $test_runner $test_dll }
}

task package {
    $web_deploy_settings =
        "/p:DeployOnBuild=true;" +
        "PublishProfile=CreatePackage"
    exec { & $msbuild $msbuild_settings $web_deploy_settings $app_proj }
}
