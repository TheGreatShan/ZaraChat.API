namespace ZaraChat.BusinessLogic.Helpers

type Transcription = { Text: string }

module ResponseProcessor =
    let toTranscription (transcriptionText: string) =
        { Text = transcriptionText }
