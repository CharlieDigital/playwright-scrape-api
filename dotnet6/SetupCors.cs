public static class SetupCorsExtension {
  public static void AddCorsConfig(
    this IServiceCollection services
  ) {
    services.AddCors(options =>
      options.AddPolicy("cors-policy", policy => policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(
          "http://localhost:3000"
          // "https://my-app.example.com"
        )));
  }
}