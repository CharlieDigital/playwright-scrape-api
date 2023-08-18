gcloud_project=thinktastic-dev
gcloud_artifact_repo=pw-scrape
gcloud_svc=pw-scrape-api-ts
gcloud_region=us-east1

# Build, tag, and push.
docker buildx build \
  --platform linux/amd64 \
  --push \
  -t $gcloud_region-docker.pkg.dev/$gcloud_project/$gcloud_artifact_repo/$gcloud_svc .

# Deploy image
gcloud run deploy $gcloud_svc \
  --image=$gcloud_region-docker.pkg.dev/$gcloud_project/$gcloud_artifact_repo/$gcloud_svc:latest \
  --allow-unauthenticated \
  --port=8080 \
  --min-instances=0 \
  --max-instances=1 \
  --cpu-boost \
  --memory=1Gi \
  --set-env-vars=