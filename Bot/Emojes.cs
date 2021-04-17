using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Emojies
{
    public class Emojs
    {
        public Emojs()
        {
            FillEmojiTable();
        }

        public static Hashtable emojies = new Hashtable();
        public static void FillEmojiTable()
        {
            emojies.Add(":ok:", "\uD83D\uDC4C");
            emojies.Add(":hashtag:", "\u0023");
            emojies.Add(":heart:", "\u2665");
            emojies.Add(":point_up_2:", "\uD83D\uDC46");
            emojies.Add(":sweet_smile:", "\uD83D\uDE05");
            emojies.Add(":smile:", "\uD83D\uDE04");
            emojies.Add(":joy:", "\uD83D\uDE02");
            emojies.Add(":wink:", "\uD83D\uDE09");
        }
    }
}
