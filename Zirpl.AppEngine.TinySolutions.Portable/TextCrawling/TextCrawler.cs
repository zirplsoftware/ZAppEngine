using System;
using System.Collections.Generic;

namespace Zirpl.AppEngine.TinySolutions.TextCrawling
{
    public class TextCrawler
    {
        private TextPosition readStart;

        public TextCrawler(String text)
        {
            if (String.IsNullOrEmpty(text)) {throw new ArgumentNullException("text");}

            this.Text = text;
            this.CurrentPosition = TextPosition.Start;
            this.ActionQueue = new ActionQueueHandler(this);

        }

        public String Text { get; private set; }
        public TextPosition CurrentPosition { get; private set; }
        public ActionQueueHandler ActionQueue { get; private set; }
        public String Substring 
        {
            get
            {
                if (this.CurrentPosition.PositionType == TextPositionType.Start)
                {
                    return this.Text;
                }
                else if (this.CurrentPosition.PositionType == TextPositionType.End)
                {
                    return null;
                }
                else
                {
                    return this.Text.Substring(this.CurrentPosition.Position.Value);
                }
            }
        }


        public void StartRead()
        {
            if (this.CurrentPosition.AtEnd) { throw new InvalidOperationException("Cannot read from end"); }
            if (readStart != null) {throw new InvalidOperationException("Cannot start a new read when one is in progress");}

            readStart = this.CurrentPosition;
        }
        public void StartReadExclusive()
        {
            if (this.CurrentPosition.AtEnd) { throw new InvalidOperationException("Cannot read from end"); }
            if (this.CurrentPosition.AtIndex && this.CurrentPosition.Position.Value + 1 == this.Text.Length) { throw new InvalidOperationException("Cannot start an exclusive read from the last position");}
            if (readStart != null) { throw new InvalidOperationException("Cannot start a new read when one is in progress"); }

            readStart = TextPosition.Next(this.CurrentPosition, this.Text);
        }
        public String EndRead()
        {
            this.ActionQueue.Execute();

            if (readStart == null) { throw new InvalidOperationException("Cannot end a read until one is in progress"); }


            //if (this.AtStart) {throw new InvalidOperationException("Cannot end read while at the start position");}

            // we know it couldn't have started at the end
            // and so it must be at the start or indexed
            // if it is at the start, 0 is the correct position, which is also the value of GetValueOrDefault()
            // so we can just use that
            int startPosition = this.readStart.Position.GetValueOrDefault();
            this.readStart = null;
            int endPosition = this.CurrentPosition.AtEnd ? this.Text.Length - 1 : (this.CurrentPosition.AtStart ? 0 : this.CurrentPosition.Position.Value);

            if (startPosition > endPosition) { throw new InvalidOperationException("Cannot read while ending position is less than starting position"); }

            int length = endPosition - startPosition + 1;
            return this.Text.Substring(startPosition, length);
        }

        public String EndReadExclusive()
        {
            String output = this.EndRead();
            if (!String.IsNullOrEmpty(output)
                && this.CurrentPosition.PositionType != TextPositionType.End)
            {
                output = output.Substring(0, output.Length - 1);
            }
            return output;
        }

        public bool CanJumpTo(int position)
        {
            if (position < 0)
            {
                return false;
                //throw new ArgumentOutOfRangeException("position", "Must be greater than or equal to 0");
            }

            // NOTE: only allowing jumping to the LAST char
            return position < this.Text.Length;
        }
        public bool CanForwardBy(int length)
        {
            if (length <= 0)
            {
                return false;
                //throw new ArgumentOutOfRangeException("length", "Must be greater than 0");
            }

            if (this.CurrentPosition.AtEnd)
            {
                return false;
            }
            else if (this.CurrentPosition.AtStart)
            {
                // NOTE: Only allowing forwarding to the LAST char
                return length <= this.Text.Length;
            }
            else
            {
                // NOTE: Only allowing forwarding to the LAST char
                return this.CurrentPosition.Position.Value + length < this.Text.Length;
            }
        }
        public bool CanBackBy(int length)
        {
            if (length <= 0)
            {
                return false;
                //throw new ArgumentOutOfRangeException("length", "Must be greater than 0");
            }

            if (this.CurrentPosition.AtStart)
            {
                return false;
            }
            else if (this.CurrentPosition.AtEnd)
            {
                // NOTE: Only allowing backing to the FIRST char
                return length <= this.Text.Length;
            }
            else
            {
                // NOTE: Only allowing backing to the FIRST char
                return this.CurrentPosition.Position.Value - length >= 0;
            }
        }

        public void JumpTo(int position)
        {
            if (!this.CanJumpTo(position)) { throw new ArgumentOutOfRangeException("position", "Must be greater than or equal to 0 and less than the length of the text");}

            this.CurrentPosition = TextPosition.Indexed(position);
        }
        public void ToStart()
        {
            this.CurrentPosition = TextPosition.Start;
        }
        public void ToFirst()
        {
            this.CurrentPosition = TextPosition.Indexed(0);
        }
        public void ToLast()
        {
            this.CurrentPosition = TextPosition.Indexed(this.Text.Length - 1);
        }
        public void ToEnd()
        {
            this.CurrentPosition = TextPosition.End;
        }

        public void ForwardBy(int length)
        {
            if (this.CurrentPosition.AtEnd) { throw new InvalidOperationException("Cannot forward past the end"); }
            if (!this.CanForwardBy(length)) { throw new ArgumentOutOfRangeException("length");}

            if (this.CurrentPosition.PositionType == TextPositionType.Start)
            {
                this.JumpTo(length - 1);
            }
            else // Indexed
            {
                this.JumpTo(this.CurrentPosition.Position.Value + length);
            }
        }
        public void BackBy(int length)
        {
            if (this.CurrentPosition.AtStart) { throw new InvalidOperationException("Cannot back past the start"); }
            if (!this.CanBackBy(length)) { throw new ArgumentOutOfRangeException("length"); }


            if (this.CurrentPosition.PositionType == TextPositionType.End)
            {
                this.JumpTo(this.Text.Length - length);
            }
            else // Indexed
            {
                this.JumpTo(this.CurrentPosition.Position.Value - length);
            }
        }

        public bool To(String token)
        {
            return this.To(token, false);
        }
        public bool To(String token, bool remainInCurrentPositionIfNotFound)
        {
            if (this.CurrentPosition.PositionType == TextPositionType.End) { throw new InvalidOperationException("Cannot forward past the end"); }

            int startPosition = this.CurrentPosition.Position ?? 0; // if it null, it is at the start position

            int position = this.Text.IndexOf(token, startPosition);
            this.CurrentPosition = position >= 0 ? TextPosition.Indexed(position) : (remainInCurrentPositionIfNotFound ? this.CurrentPosition : TextPosition.End);
            return position >= 0;
        }
        //public bool To(char token)
        //{
        //    return this.To(token, false);
        //}
        //public bool To(char token, bool remainInCurrentPositionIfNotFound)
        //{
        //    return this.To(token.ToString(), remainInCurrentPositionIfNotFound);
        //}
        //public bool ForwardToAny(char[] tokens)
        //{
        //    return this.ForwardToAny(tokens, false);
        //}
        //public bool ForwardToAny(char[] tokens, bool remainInCurrentPositionIfNotFound)
        //{
        //    if (this.CurrentPosition.PositionType == TextPositionType.End) { throw new InvalidOperationException("Cannot forward past the end"); }

        //    int startPosition = this.CurrentPosition.Position ?? 0; // if it null, it is at the start position

        //    int position = this.Text.IndexOfAny(tokens, startPosition);
        //    this.CurrentPosition = position >= 0 ? TextPosition.Indexed(position) : (remainInCurrentPositionIfNotFound ? this.CurrentPosition : TextPosition.End);
        //    return position >= 0;
        //}

        public bool ToLast(String token)
        {
            return this.ToLast(token, false);
        }
        public bool ToLast(String token, bool remainInCurrentPositionIfNotFound)
        {
            if (this.CurrentPosition.PositionType == TextPositionType.Start) { throw new InvalidOperationException("Cannot back past the start"); }

            int startPosition = this.CurrentPosition.Position ?? this.Text.Length - 1; // if it null, it is at the end position

            int position = this.Text.LastIndexOf(token, startPosition);
            this.CurrentPosition = position >= 0 ? TextPosition.Indexed(position) : (remainInCurrentPositionIfNotFound ? this.CurrentPosition : TextPosition.Start);
            return position >= 0;
        }
        //public bool ToLast(char token)
        //{
        //    return ToLast(token, false);
        //}
        //public bool ToLast(char token, bool remainInCurrentPositionIfNotFound)
        //{
        //    return ToLast(token.ToString(), remainInCurrentPositionIfNotFound);
        //}
        //public bool BackToAny(char[] tokens)
        //{
        //    return this.BackToAny(tokens, false);
        //}
        //public bool BackToAny(char[] tokens, bool remainInCurrentPositionIfNotFound)
        //{
        //    if (this.CurrentPosition.PositionType == TextPositionType.Start) { throw new InvalidOperationException("Cannot back past the start"); }

        //    int startPosition = this.CurrentPosition.Position ?? this.Text.Length - 1; // if it null, it is at the end position

        //    int position = this.Text.LastIndexOfAny(tokens, startPosition);
        //    this.CurrentPosition = position >= 0 ? TextPosition.Indexed(position) : (remainInCurrentPositionIfNotFound ? this.CurrentPosition : TextPosition.Start);
        //    return position >= 0;
        //}

        public bool ToEndOf(String token)
        {
            return this.ToEndOf(token, false);
        }
        public bool ToEndOf(String token, bool remainInCurrentPositionIfNotFound)
        {
            bool found = this.To(token, remainInCurrentPositionIfNotFound);
            if (found)
            {
                this.ForwardBy(token.Length - 1);
            }
            return found;
        }

        public bool ToEndOfLast(String token)
        {
            return this.ToEndOfLast(token, false);
        }
        public bool ToEndOfLast(String token, bool remainInCurrentPositionIfNotFound)
        {
            bool found = this.ToLast(token, remainInCurrentPositionIfNotFound);
            if (found)
            {
                this.ForwardBy(token.Length - 1);
            }
            return found;
        }



        //public bool ForwardToNext();
        //public bool BackToLast();

        //public String EndReadExclusive();

        
        // NOTE: these should all be overloaded with remainInCurrentPositionIfNotFound

        //public String ReadTo(String token);
        //public String ReadTo(char token);
        //public String ReadToAny(char[] tokens);

        //public String ReadToEndOf(String token);
        //public String ReadToEndOf(char token);
        //public String ReadToEndOfAny(char[] tokens);

        //public String ReadToExclusive(String token);
        //public String ReadToExclusive(char token);
        //public String ReadToAnyExclusive(char[] tokens);

        //public String ReadToEndOfExclusive(String token);
        //public String ReadToEndOfExclusive(char token);
        //public String ReadToEndOfAnyExclusive(char[] tokens);

        public class ActionQueueHandler
        {
            private Queue<Delegate> actionQueue;
            private TextCrawler crawler;

            internal ActionQueueHandler(TextCrawler crawler)
            {
                this.crawler = crawler;
                this.actionQueue = new Queue<Delegate>();
            }

            public String EndRead()
            {
                return this.crawler.EndRead();
            }
            public String EndReadExclusive()
            {
                return this.crawler.EndReadExclusive();
            }

            public bool Execute()
            {
                bool haveAnyFailed = false;
                while (this.actionQueue.Count > 0)
                {
                    Delegate dDelegate = this.actionQueue.Dequeue();
                    if (dDelegate is Action)
                    {
                        ((Action) dDelegate).Invoke();
                    }
                    else if (dDelegate is Func<bool>)
                    {
                        haveAnyFailed = haveAnyFailed || !((Func<bool>) dDelegate).Invoke();
                    }
                    else
                    {
                        throw new Exception("Unexpected object in the ActionQueue");
                    }
                }

                // TODO: what to do with haveAnyFailed

                this.actionQueue.Clear();
                return !haveAnyFailed;
            }

            public ActionQueueHandler StartRead()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.StartRead()));
                return this;
            }
            public ActionQueueHandler StartReadExclusive()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.StartReadExclusive()));
                return this;
            }

            public ActionQueueHandler JumpTo(int position)
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.JumpTo(position)));
                return this;
            }

            public ActionQueueHandler ToStart()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.ToStart()));
                return this;
            }

            public ActionQueueHandler ToFirst()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.ToFirst()));
                return this;
            }

            public ActionQueueHandler ToLast()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.ToLast()));
                return this;
            }

            public ActionQueueHandler ToEnd()
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.ToEnd()));
                return this;
            }

            public ActionQueueHandler ForwardBy(int length)
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.ForwardBy(length)));
                return this;
            }

            public ActionQueueHandler BackBy(int length)
            {
                this.actionQueue.Enqueue(new Action(() => this.crawler.BackBy(length)));
                return this;
            }

            public ActionQueueHandler To(String token)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.To(token)));
                return this;
            }

            public ActionQueueHandler To(String token, bool remainInCurrentPositionIfNotFound)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.To(token, remainInCurrentPositionIfNotFound)));
                return this;
            }

            public ActionQueueHandler ToLast(String token)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.ToLast(token)));
                return this;
            }

            public ActionQueueHandler ToLast(String token, bool remainInCurrentPositionIfNotFound)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.ToLast(token, remainInCurrentPositionIfNotFound)));
                return this;
            }

            public ActionQueueHandler ToEndOf(String token)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.ToEndOf(token)));
                return this;
            }

            public ActionQueueHandler ToEndOf(String token, bool remainInCurrentPositionIfNotFound)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.ToEndOf(token, remainInCurrentPositionIfNotFound)));
                return this;
            }

            public ActionQueueHandler ToEndOfLast(String token)
            {
                this.actionQueue.Enqueue(new Func<Boolean>(() => this.crawler.ToEndOfLast(token)));
                return this;
            }

            public ActionQueueHandler ToEndOfLast(String token, bool remainInCurrentPositionIfNotFound)
            {
                this.actionQueue.Enqueue(
                    new Func<Boolean>(() => this.crawler.ToEndOfLast(token, remainInCurrentPositionIfNotFound)));
                return this;
            }
        }
    }
}
