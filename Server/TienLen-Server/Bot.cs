using System.Collections.Generic;
using System.Linq;
using System;

namespace TienLen_Server
{
    class Bot
    {
        private List<Card> BaiDangGiu = new List<Card>();
        private string LastTurnCard = "";
        private string data = "";
        public string username = "Mr.Robot";
        private Dem thongKe;
        private string[] Hang;
        public Bot()
        {
           
        }
        public Bot(List<Card> cards)
        {
            this.BaiDangGiu = cards;
            thongKe = new Dem(cards);
            ThongKe();
        }
        public void XaBai()
        {
            if (BaiDangGiu != null)
                BaiDangGiu.Clear();
        }

        public string ReceiveData(string data)
        {
            //
            Console.WriteLine("data: " + data);
            Console.WriteLine("bai con lai: ");
            foreach (Card card in BaiDangGiu)
                Console.Write(card.Information + " ");
            Console.WriteLine();
            //
            this.data = data;
            string[] exeData = data.Split('|');
            if (exeData[0] == "vanmoi")
            {
                this.BaiDangGiu.Clear();
                LastTurnCard = "";
                for (int i = 1; i < exeData.Length; i++)
                    this.BaiDangGiu.Add(new Card(exeData[i]));
                this.BaiDangGiu.Sort((x, y) => x.CompareTo(y));
                thongKe = new Dem(BaiDangGiu);
                ThongKe();
            }
            else if (exeData[0] == "turn")
            {
                if (exeData[1] == this.username)
                {
                    Console.WriteLine(LastTurnCard);
                    if (LastTurnCard == "")
                        return DanhBai("boluot");
                    else
                        return DanhBai(LastTurnCard);
                }

            }
            else if (exeData[0] == "bai")
            {
                LastTurnCard = data.Remove(0, 4);

            }
            else if (exeData[0] == "newturn")
            {
                LastTurnCard = "";

            }
            return "";
        }

        public string DanhBai(string lastTurnCard)
        {
            string result="boluot";
            if (lastTurnCard == "boluot")
            {
                if (TimSanhDaiNhat() != "boluot")
                    result = TimSanhDaiNhat();
                else if (TimXamNhoNhat() != "boluot")
                    result = TimXamNhoNhat();
                else if (TimLeNhoNhat() != "boluot")
                    result = TimLeNhoNhat();
                else
                    result = TimDoiNhoNhat();
            }
            else
                result = ChonBai(lastTurnCard);
            if (result != "boluot")
            {
                string[] data = result.Split('|');
                string[] data1;
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < BaiDangGiu.Count; j++)
                    {
                        if (BaiDangGiu[j].Information == data[i])
                        {
                            BaiDangGiu.Remove(BaiDangGiu[j]);
                            break;
                        }

                    }
                    data1 = data[i].Split('_');
                    for (int j = 0; j < thongKe.array[int.Parse(data1[0]) - 3].Count; j++)
                    {
                        if (thongKe.array[int.Parse(data1[0]) - 3][j].Information == data[i])
                        {
                            thongKe.array[int.Parse(data1[0]) - 3].Remove(thongKe.array[int.Parse(data1[0]) - 3][j]);
                            break;
                        }
                    }
                }
            }
            return "bai|" + result;
        }
        private string TimSanh(List<Card> lastTurnCard)
        {
            string result = ""; // Gia tri tra ve
            int i, j;
            CardType a = new CardType(lastTurnCard);
            int n = 0;
            for (i = lastTurnCard[0].Rank - 3; i < 13 - a.count; i++)
            {
                if (thongKe.array[i] == null)
                {
                    n = 0;
                    continue;
                }
                else
                {
                    n++;
                    if (n == a.count && thongKe.array[i][thongKe.array[i].Count - 1] > a.gr)
                    {
                        for (j = i - a.count + 1; j < i; j++)
                            result += "|" + thongKe.array[j][0].Information;
                        result += "|" + thongKe.array[j][thongKe.array[i].Count - 1].Information;
                        return result.Remove(0, 1);
                    }
                    else if (n == a.count && thongKe.array[i][thongKe.array[i].Count - 1] < a.gr)
                    {
                        n = 0;
                        i = i - a.count + 1;
                    }
                }
            }
            return "0";
        }
        private string TimTuQuy(int rank = 0)
        {
            string result = ""; // Gia tri tra ve
            int i, j;
            if (rank < 3 || rank > 14)
            {
                for (i = 0; i < 13; i++)
                {
                    if (thongKe.array[i] != null)
                    {
                        if (thongKe.array[i].Count == 4)
                        {
                            for (j = 0; j < 4; j++)
                                result += "|" + thongKe.array[i][j].Information;
                            return result.Remove(0, 1);
                        }
                    }
                }
            }
            else
            {
                for (i = rank; i < 13; i++)
                {
                    if (thongKe.array[i] != null)
                    {
                        if (thongKe.array[i].Count == 4)
                        {
                            for (j = 0; j < 4; j++)
                                result += "|" + thongKe.array[i][j].Information;
                            return result.Remove(0, 1);
                        }
                    }
                }
            }
            return "0";
        }
        private string Tim3DoiThong()
        {
            string result = ""; // Gia tri tra ve
            int i, j;
            int n = 0;
            for (i = 0; i < 12; i++)
            {
                if (thongKe.array[i] == null)
                {
                    n = 0;
                    continue;
                }
                else
                {
                    if (thongKe.array[i].Count >= 2)
                        n++;
                    if (n == 3)
                    {
                        for (j = i - 2; j <= i; j++)
                            result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                        return result.Remove(0, 1);
                    }
                }
            }
            return "0";
        }
        private string Tim4DoiThong()
        {
            string result = ""; // Gia tri tra ve
            int i, j;
            int n = 0;
            for (i = 0; i < 12; i++)
            {
                if (thongKe.array[i] == null)
                {
                    n = 0;
                    continue;
                }
                else
                {
                    if (thongKe.array[i].Count >= 2)
                        n++;
                    else
                    {
                        n = 0;
                        continue;
                    }
                    if (n == 4)
                    {
                        for (j = i - 3; j <= i; j++)
                            result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                        return result.Remove(0, 1);
                    }
                }
            }
            return "0";
        }
        private void ThongKe()       // Thong ke 4 doi thong, tu quy, 3 doi thong
        {
            Hang = new string[13];
            for (int j = 0; j < 13; j++)
                Hang[j] = "";
            int i = 0;
            string temp = Tim4DoiThong();
            string[] data;
            int flag = 0;
            if (temp != "0")
            {
                flag = 1;
                data = temp.Split('|');
                foreach (string str in data)
                    Hang[i++] = str;

            }
            while ((temp = TimTuQuy(int.Parse(temp.Substring(0, 1)) + 1)) != "0")
            {
                data = temp.Split('|');
                foreach (string str in data)
                    Hang[i++] = str;
            }
            if (flag == 0)
            {
                temp = Tim3DoiThong();
                if (temp != "0")
                {
                    data = temp.Split('|');
                    foreach (string str in data)
                        Hang[i++] = str;
                }
            }
        }
        private bool ktLeVaDoi(string s)
        {
            string[] data = s.Split('|');
            int rank = int.Parse(data[0].Split('_')[0]);
            if (Hang.Contains(data[0]))
                return false;
            else if (rank < 12 && thongKe.array[rank - 2] != null && thongKe.array[rank - 1] != null && thongKe.array[rank] != null)
            {
                if ((thongKe.array[rank - 2].Count >= 2 && thongKe.array[rank - 1].Count >= 2 && thongKe.array[rank].Count >= 2) || thongKe.array[rank - 2].Count == 1 || thongKe.array[rank - 1].Count == 1)
                    return false;
                else
                    return true;
            }
            else if (rank < 13 && thongKe.array[rank - 2] != null && thongKe.array[rank - 1] != null)
                if ((thongKe.array[rank - 2].Count >= 2 && thongKe.array[rank - 1].Count >= 2) || thongKe.array[rank - 2].Count == 1 || thongKe.array[rank - 1].Count == 1)
                    return false;
            return true;
        }
        private string ChonBai(string lastTurnCard1)
        {

            string[] data = lastTurnCard1.Split('|');
            List<Card> lastTurnCard = new List<Card>();
            foreach (string str in data)
                lastTurnCard.Add(new Card(str));
            string result = ""; // Gia tri tra ve
            int i, j;
            CardType a = new CardType(lastTurnCard);
            if (a.type == 1)
            {
                if (a.count > 2) //kiem tra xam vs tu quy
                {
                    for (i = lastTurnCard[0].Rank - 2; i < 13; i++)
                    {
                        if (thongKe.array[i] != null)
                            if (thongKe.array[i].Count == a.count)
                                break;
                    }
                    if (i == 13)
                    {
                        if (a.count == 4)   // Chat tu quy
                        {
                            result = Tim4DoiThong();
                            if (result == "0") return "boluot";
                            return result;
                        }
                    }
                    for (j = 0; j < a.count; j++)
                        result += "|" + thongKe.array[i][j].Information;
                    return result.Remove(0, 1);
                }
                else // kierm tra le voi doi
                {

                    for (i = lastTurnCard[0].Rank - 3; i < 13; i++)
                    {
                        if (thongKe.array[i] != null)
                            if (thongKe.array[i].Count == a.count && a.gr < thongKe.array[i][thongKe.array[i].Count - 1])
                                break;
                    }
                    if (i == 13) // tim k ra
                    {
                        for (i = lastTurnCard[0].Rank - 3; i < 13; i++)
                        {
                            if (thongKe.array[i] != null)
                                if (thongKe.array[i].Count > a.count)
                                    break;
                        }
                        if (i == 13)
                        {
                            if (a.gr.Rank == 15)
                            {
                                if (a.count == 1)
                                {
                                    result = Tim3DoiThong();
                                    if (result == "0")
                                        result = TimTuQuy();
                                    if (result == "0")
                                        return "boluot";
                                    return result;
                                }
                                else if (a.count == 2)
                                {
                                    result = TimTuQuy();
                                    if (result == "0")
                                        result = Tim4DoiThong();
                                    if (result == "0")
                                        return "boluot";
                                    return result;
                                }
                            }
                        }
                        return "boluot";
                    }
                    for (j = 0; j < a.count; j++)
                        result += "|" + thongKe.array[i][j].Information;
                    result = result.Remove(0, 1);
                    do
                    {
                        if (result == "boluot")
                            return "boluot";
                        else if (ktLeVaDoi(result) == false)
                        {
                            result = ChonBai(result);
                        }
                        else
                            return result;
                    } while (true);
                }
            }
            else if (a.type == 2)
            {
                result = TimSanh(lastTurnCard);
                if (result == "0")
                    return "boluot";
                while (Hang.Contains(result.Substring(0, result.IndexOf('|'))))
                {
                    result = ChonBai(result);
                    if (result == "boluot")
                        return "boluot";
                }
                return result;
            }
            else
            {
                if (a.count == 3)
                {
                    int n = 0;
                    for (i = 0; i < 12; i++)
                    {
                        if (thongKe.array[i] == null)
                        {
                            n = 0;
                            continue;
                        }
                        else
                        {
                            if (thongKe.array[i].Count >= 2)
                                n++;
                            if (n == 3 && thongKe.array[i][thongKe.array[i].Count - 1] > a.gr)
                            {
                                for (j = i - 2; j < i; j++)
                                    result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                                result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][thongKe.array[i].Count - 1].Information;
                                return result.Remove(0, 1);
                            }
                            else if (n == 3 && thongKe.array[i][thongKe.array[i].Count - 1] < a.gr)
                            {
                                if (thongKe.array[i + 1] == null)
                                {
                                    n = 0;
                                    continue;
                                }
                                else
                                {
                                    if (thongKe.array[i + 1].Count < 2)
                                    {
                                        n = 0;
                                        continue;

                                    }
                                    if (thongKe.array[i + 1].Count >= 2)
                                    {
                                        for (j = i - 2; j <= i; j++)
                                            result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                                        result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                                        return result.Remove(0, 1);
                                    }
                                }
                            }
                        }
                    }
                    result = TimTuQuy();
                    if (result == "0")
                        return "boluot";
                    return result;
                }
                else                    // 4 doi thong
                {
                    int n = 0;
                    for (i = 0; i < 12; i++)
                    {
                        if (thongKe.array[i] == null)
                        {
                            n = 0;
                            continue;
                        }
                        else
                        {
                            if (thongKe.array[i].Count >= 2)
                                n++;
                            if (n == 4 && thongKe.array[i][thongKe.array[i].Count - 1] > a.gr)
                            {
                                for (j = i - 2; j <= i; j++)
                                    result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][1].Information;
                                result += "|" + thongKe.array[j][0].Information + "|" + thongKe.array[j][thongKe.array[i].Count - 1].Information;
                                return result.Remove(0, 1);
                            }
                        }
                    }
                    return "boluot";
                }
            }
        }
        private string TimSanhDaiNhat()
        {
            string result = "";
            int count = 0, maxCount = 2, index = 0;
            for (int i = 0; i < 12; i++)
            {
                if (thongKe.array[i] == null)
                {
                    if (count > maxCount)
                    {
                        maxCount = count;
                        index = i;
                    }
                    count = 0;
                    continue;
                }
                count++;
            }
            if (maxCount != 2)
            {
                for (int i = index - maxCount; i < index; i++)
                {
                    result += "|" + thongKe.array[i][0].Information;
                }
            }
            if (result != "")
                return result.Remove(0, 1);
            return "boluot";
        }
        private string TimLeNhoNhat()
        {
            string result = "";
            for (int i = 0; i < 12; i++)
            {
                if (thongKe.array[i] != null && thongKe.array[i].Count == 1)
                {
                    result = thongKe.array[i][0].Information;
                    break;
                }
            }
            if (result != "")
                return result;
            return "boluot";
        }
        private string TimDoiNhoNhat()
        {
            string result = "";
            for (int i = 0; i < 12; i++)
            {
                if (thongKe.array[i] != null && thongKe.array[i].Count == 2)
                {
                    result = thongKe.array[i][0].Information + "|" + thongKe.array[i][1].Information;
                    break;
                }
            }
            if (result != "")
                return result;
            return "boluot";
        }
        private string TimXamNhoNhat()
        {
            string result = "";
            for (int i = 0; i < 9; i++)
            {
                if (thongKe.array[i] != null && thongKe.array[i].Count == 3)
                {
                    result += thongKe.array[i][0].Information + "|" + thongKe.array[i][1].Information + "|" + thongKe.array[i][2].Information;
                    break;
                }
            }
            if (result != "")
                return result;
            return "boluot";
        }
    }
}


