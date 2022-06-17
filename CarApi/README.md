# Push Images to Docker Hub
1. CarApi

docker build -f "C:\Users\martynas.samuilovas\source\Personal\CarApi\CarApi\Dockerfile" --force-rm -t cytra/carapi --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=CarApi" "C:\Users\martynas.samuilovas\source\Personal\CarApi"
docker image push cytra/carapi

2. CarScripts

cd C:\Users\martynas.samuilovas\source\Personal\CarApi\CarApi.Scripts
docker build . -t cytra/carscripts 
docker image push cytra/carscripts

docker login -u cytra    #Optional


#Start APP

docker run -d -p 4444:4444 --shm-size="2g" selenium/standalone-chrome:4.2.2-20220609



