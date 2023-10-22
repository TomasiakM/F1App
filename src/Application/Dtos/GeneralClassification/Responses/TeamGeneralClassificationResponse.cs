using Application.Dtos.Team.Responses;

namespace Application.Dtos.GeneralClassification.Responses;
public record TeamGeneralClassificationResponse(
    int Place,
    float Points,
    TeamResponse Team);
