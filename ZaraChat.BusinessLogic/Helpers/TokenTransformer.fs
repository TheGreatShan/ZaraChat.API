namespace ZaraChat.BusinessLogic.Helpers

module TokenTransformer =
    let removeBearer (token: string) =
        token.Replace("Bearer ", "")
