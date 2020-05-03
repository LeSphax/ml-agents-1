#!/bin/bash
mlagents-learn config/slideball.yaml --curriculum=config/curricula/slideball-episodic  --slow --run-id=$1 $2 $3 $4