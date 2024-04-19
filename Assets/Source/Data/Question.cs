using System;

namespace Source.Data
{
    [Serializable]
    public struct Question
    {
        public string question;
        public Answer[] answers;
        public string background;
    }
}
