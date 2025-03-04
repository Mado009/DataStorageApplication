using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class UploadRequest {
    [Required(ErrorMessage = "File is required.")]
    [FileExtensions(Extensions = "jpg,jpeg,png,pdf,doc,docx", ErrorMessage = "Invalid file type. Allowed types: jpg, jpeg, png, pdf, doc, docx.")]
    public IFormFile File { get; set; }

    [Required(ErrorMessage = "Dropdown selection is required.")]
    public string Dropdown { get; set; }

    [Required(ErrorMessage = "Text input is required.")]
    [MaxLength(100, ErrorMessage = "Text input cannot exceed 100 characters.")]
    public string InputText { get; set; }

    [Required(ErrorMessage = "Free text is required.")]
    [MaxLength(500, ErrorMessage = "Free text cannot exceed 500 characters.")]
    public string FreeText { get; set; }
}
