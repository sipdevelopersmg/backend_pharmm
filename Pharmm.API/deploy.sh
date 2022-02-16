#!/usr/bin/env bash
echo "############# starting deployment... ##############"
./versioning.sh

BUILD_DATE=$(sed -n 's/^BUILD_DATE=\(.*\)/\1/p' < .env)
BUILD_VERSION=$(sed -n 's/^BUILD_VERSION=\(.*\)/\1/p' < .env)

IMAGE_NAME=his/pharmm_api:$BUILD_DATE.$BUILD_VERSION

echo "1. building new image : $IMAGE_NAME"

docker-compose build

echo "2. update container with new image : $IMAGE_NAME"

docker-compose up -d

echo "############# deployment completed ... ##############"

# author : Andre Kurniawan
# email  : andrekurniawan@gmail.com
# deploy from ci/cd




