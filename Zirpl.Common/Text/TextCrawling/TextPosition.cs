using System;

namespace Zirpl.Text.TextCrawling
{
    public sealed class TextPosition
    {
        private TextPosition()
        {
        }

        public TextPositionType PositionType { get; private set; }
        public int? Position { get; private set; }

        public bool AtStart
        {
            get { return this.PositionType == TextPositionType.Start; }
        }
        public bool AtEnd
        {
            get { return this.PositionType == TextPositionType.End; }
        }
        public bool AtIndex
        {
            get { return this.PositionType == TextPositionType.Indexed; }
        }

        internal static TextPosition Start
        {
            get { return new TextPosition() { PositionType = TextPositionType.Start }; }
        }
        internal static TextPosition End
        {
            get { return new TextPosition() { PositionType = TextPositionType.End }; }
        }
        internal static TextPosition Indexed(int position)
        {
            if (position < 0) { throw new ArgumentOutOfRangeException("position"); }

            return new TextPosition() { PositionType = TextPositionType.Indexed, Position = position };
        }
        internal static TextPosition Next(TextPosition position, String text)
        {
            if (position.PositionType == TextPositionType.End) { throw new ArgumentOutOfRangeException("position");}

            if (position.PositionType == TextPositionType.Start)
            {
                return Indexed(0);
            }
            else
            {
                if (position.Position.Value + 1 == text.Length)
                {
                    return End;
                }
                else
                {
                    return Indexed(position.Position.Value + 1);
                }
            }
        }
        internal static TextPosition Previous(TextPosition position, String text)
        {
            if (position.PositionType == TextPositionType.Start) { throw new ArgumentOutOfRangeException("position"); }

            if (position.PositionType == TextPositionType.End)
            {
                return Indexed(text.Length - 1);
            }
            else
            {
                if (position.Position.Value == 0)
                {
                    return Start;
                }
                else
                {
                    return Indexed(position.Position.Value - 1);
                }
            }
        }
    }
}
