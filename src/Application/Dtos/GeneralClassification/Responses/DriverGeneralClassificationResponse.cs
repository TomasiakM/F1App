using Application.Dtos.Driver.Responses;

namespace Application.Dtos.GeneralClassification.Responses;
public record DriverGeneralClassificationResponse(
    int Place,
    float Points, 
    DriverResponse Driver);
