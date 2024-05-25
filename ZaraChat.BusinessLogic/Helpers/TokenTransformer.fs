namespace ZaraChat.BusinessLogic.Helpers

module TokenTransformer =
    let RemoveBearer (token: string) =
        token.Replace("Bearer ", "")
