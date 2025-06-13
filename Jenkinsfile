pipeline {
    agent any
 
    environment {
        DOTNET_ROOT = "${HOME}/.dotnet"
        PATH = "${env.PATH}:${env.DOTNET_ROOT}:${env.DOTNET_ROOT}/tools"
        IMAGE_NAME = 'hello-world-dotnet'
        VERSION = '1.0.${BUILD_NUMBER}'
    }
 
    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/dnkudale/HelloWorldApp.git'
            }
        }
 
        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }
 
        stage('Build') {
            steps {
                sh 'dotnet build --no-restore -c Release'
            }
        }
 
        stage('Test') {
            steps {
                sh 'dotnet test --no-build --verbosity normal'
            }
        }
 
        stage('Publish Artifacts') {
            steps {
                sh 'dotnet publish -c Release -o out'
                archiveArtifacts artifacts: 'out/**', fingerprint: true
            }
        }
 
        stage('Docker Build') {
            steps {
                sh 'docker build -t ${IMAGE_NAME}:${VERSION} .'
            }
        }
 
        stage('Docker Push') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'docker-hub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    sh '''
                        echo "$DOCKER_PASS" | docker login -u "$DOCKER_USER" --password-stdin
                        docker tag ${IMAGE_NAME}:${VERSION} ${DOCKER_USER}/${IMAGE_NAME}:${VERSION}
                        docker push ${DOCKER_USER}/${IMAGE_NAME}:${VERSION}
                    '''
                }
            }
        }
 
        stage('Deploy to Server') {
            steps {
                sshagent(['deploy-ssh-key']) {
                    sh 'ssh user@target-server "docker pull youruser/hello-world-dotnet:${VERSION} && docker run -d -p 5000:80 youruser/hello-world-dotnet:${VERSION}"'
                }
            }
        }
    }
 
    post {
        success {
            echo 'Pipeline completed successfully 🎉'
        }
        failure {
            echo 'Pipeline failed ❌'
        }
    }
}
