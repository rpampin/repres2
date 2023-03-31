#Repres

#Deploy on Heroku
heroku container:login // LOGIN TO HEROKU
docker build . -t registry.heroku.com/oura-ring-export/web -f .\src\Server\Dockerfile // BUILD THE APP AS A DOCKER IMAGE
docker push registry.heroku.com/oura-ring-export/web // PUSH THE IMAGE TO HEROKU REGISTRY
heroku container:release web -a oura-ring-export // RELEASE THE APPLICATION WITH NEW REGISTRY
heroku restart -a oura-ring-export // IF YOU EVER NEED TO RESTART THE APPLICATION
heroku logs --tail -a oura-ring-export // HERE TO SEE THE LOGS