#!/usr/bin/env sh
set -e

# useage: /bin/bash deploy.sh export-folder-name
# example: /bin/bash deploy.sh 210415-1035

HOST="nwdl"
DEPLOY_TO="/var/www/domicile/app"

echo "Clear Old Static Files"
ssh $HOST "rm -rf ${DEPLOY_TO}/*"

echo "Copy New Files"
scp -r ./Data/${1}/* $HOST:$DEPLOY_TO
scp ./style.override.css $HOST:$DEPLOY_TO

echo "Modify Style Reference"
# ssh $HOST "sed -i 's/TemplateData\/style.css/style.override.css/g' ${DEPLOY_TO}/index.html"
ssh $HOST "sed -i 's/<title>Unity WebGL Player | domicile<\/title>/<title>Domicile Web-Player<\/title><link rel=\"stylesheet\" href=\"style.override.css\">/g' ${DEPLOY_TO}/index.html"
