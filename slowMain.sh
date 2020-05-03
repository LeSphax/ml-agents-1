#!/bin/bash
set -x
mlagents-learn config/$1.yaml --curriculum=config/curricula/$1-size --env=Builds/$1/$2/$1$2 --slow --worker-id=$(($RANDOM % 1000)) --run-id=$3 $4 $5 $6