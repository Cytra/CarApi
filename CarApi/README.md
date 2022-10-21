#BUILD
#Push Images to Docker Hub
1. CarApi

cd C:\Users\martynas.samuilovas\source\Personal\CarApi
docker build . -t cytra/carapi 
docker image push cytra/carapi

2. CarScripts

cd C:\Users\martynas.samuilovas\source\Personal\CarApi\CarApi.Scripts
docker build . -t cytra/carscripts 
docker image push cytra/carscripts

docker login -u cytra    #Optional




#START
#Start APP locally

1. docker run -d -p 4444:4444 --shm-size="2g" selenium/standalone-chrome:4.2.2-20220609


#Start APP on k8s
1. docker run -d -p 4444:4444 --shm-size="2g" selenium/standalone-chrome:4.2.2-20220609
2. cd C:\Users\martynas.samuilovas\Desktop\Kubernetes
kubectl apply -f carscript-deployment.yaml

#Port forward car API
kubectl port-forward carapi 5001:80
http://127.0.0.1:5001/swagger/index.html


kubectl port-forward carscripts 4449:4449



#Available services
http://localhost:4448
http://127.0.0.1:5001/swagger/index.html



