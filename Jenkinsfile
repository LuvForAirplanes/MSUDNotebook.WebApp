pipeline {
  agent any
  stages {
    stage('Update Submodules') {
      steps {
        bat '''git submodule update --init --recursive'''
      }
    }
    stage('Restore Packages') {
      parallel {
        stage('Restore Dotnet Packages') {
          steps {
            bat '''cd ./msudnotebook.cloud/msudnotebook.cloud
dotnet restore'''
          }
        }
      }
    }
    stage('Build') {
      steps {
        bat '''cd ./msudnotebook.cloud/msudnotebook.cloud
dotnet build'''
      }
    }
    stage('Publish') {
      steps {
        bat '''cd ./msudnotebook.cloud/msudnotebook.cloud
dotnet publish -c Release --self-contained --runtime "linux-x64"'''
      }
    }
    stage('Deploy to msudnotebook.cloud') {
      when {
        branch 'master'
      }
      steps {
        bat "C:/tools/deploy_to_web01.bat ${WORKSPACE}/msudnotebook.cloud/msudnotebook.cloud/bin/Release/netcoreapp2.2/linux-x64/publish/* msudnotebook.cloud develop"
      }
    }
  }
  environment {
    ASPNETCORE_ENVIRONMENT = 'Staging'
  }
}
