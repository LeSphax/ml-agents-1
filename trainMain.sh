#!/bin/bash
set -x
#mlagents-learn config/$1.yaml --curriculum=config/curricula/$1-$3 --env=Builds/$1/$2/$1$2 --train --keep-checkpoints=50 --worker-id=$(($RANDOM % 1000)) --run-id=$4 $5 $6
mlagents-learn config/$1.yaml --env=Builds/$1/$2/$1$2 --train # --keep-checkpoints=50 --worker-id=$(($RANDOM % 1000)) --run-id=$3