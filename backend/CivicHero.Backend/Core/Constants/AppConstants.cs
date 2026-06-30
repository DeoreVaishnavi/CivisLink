namespace CivicHero.Backend.Core.Constants;

/// <summary>
/// Contains application-wide constant values used throughout CivicHero.
/// Avoid hard-coded strings and numbers by referencing these constants.
/// </summary>
public static class AppConstants
{
    /// <summary>
    /// The default authentication scheme used by the application.
    /// </summary>
    public const string AuthenticationScheme = "Bearer";

    /// <summary>
    /// The default database connection string name.
    /// </summary>
    public const string DefaultConnection = "DefaultConnection";

    /// <summary>
    /// The default API version.
    /// </summary>
    public const string ApiVersion = "v1";

    /// <summary>
    /// The default page number used for pagination.
    /// </summary>
    public const int DefaultPageNumber = 1;

    /// <summary>
    /// The default page size used for pagination.
    /// </summary>
    public const int DefaultPageSize = 20;

    /// <summary>
    /// The maximum page size allowed for pagination.
    /// </summary>
    public const int MaximumPageSize = 100;

    /// <summary>
    /// Supported image file extensions.
    /// </summary>
    public static readonly string[] SupportedImageExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    /// <summary>
    /// Maximum upload size (10 MB).
    /// </summary>
    public const long MaximumUploadSizeBytes = 10 * 1024 * 1024;
}