gcloud_svc=pw-scrape-api-ts

# Deploy source into a Cloud Build pipeline
gcloud run deploy $gcloud_svc \
  --source=. \
  --allow-unauthenticated \
  --port=8080 \
  --min-instances=0 \
  --max-instances=1 \
  --cpu-boost \
  --memory=1Gi \
  --set-env-vars=