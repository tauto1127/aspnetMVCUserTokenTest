using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using webUserLoginTest.Models;

namespace webUserLoginTest;

public class Sessions
{
    private static Dictionary<byte[], Session> _sessions = new Dictionary<byte[], Session>(new ByteArrayComparer());
    private static Random _random = new Random();

    /*
     * sessionidの算出方法
     * 算出したdatetime + ユーザーID + パスワード + ランダム数字をハッシュ関数に投げる
     */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>セッションid</returns>
    public static byte[] Add(User user)
    {
        byte[] sessionId;
        Console.WriteLine("セッションに追加：");

        DateTime now = DateTime.Now;
        using (var hmac = SHA256.Create())
        {
            sessionId = hmac.ComputeHash(Encoding.ASCII.GetBytes(now.ToString() + user.Id + " " + _random.Next()));
        }
        foreach (var b in sessionId)
        {
            Console.WriteLine(b);
        }
        _sessions.Add(sessionId, new Session(
            userId: user.Id, expiration: now.AddMinutes(2), acquisition: now));

        return sessionId;
    }

    public static int GetUserId(byte[] sessionId)
    {
        if (!_sessions.ContainsKey(sessionId)) return -1;
        Session session = _sessions[sessionId];
        if (session.expiration < DateTime.Now)
        {
            Console.WriteLine("セッションの削除");
            _sessions.Remove(sessionId);
            return -1;
        };

        return session.userId;
    }
    //var userId = Sessions.GetUserId(Encoding.Unicode.GetBytes(System.Net.WebUtility.UrlDecode(sessionid)));
    public static byte[] ConvertSessionIdToByte(string sessionId)
    {
        return Encoding.Unicode.GetBytes(System.Net.WebUtility.UrlDecode(sessionId));
    }

    public static void Remove(byte[] sessionId)
    {
        _sessions.Remove(sessionId);
    }
}
public class ByteArrayComparer : IEqualityComparer<byte[]>
{
    public bool Equals(byte[]? left, byte[]? right)
    {
        if (left == null || right == null)
        {
            return left == right;
        }
        if (left.Length != right.Length)
        {
            return false;
        }
        for (int i = 0; i < left.Length; i++)
        {
            if (left[i] != right[i])
            {
                return false;
            }
        }
        return true;
    }
    public int GetHashCode(byte[] key)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));
        int sum = 0;
        foreach (byte cur in key)
        {
            sum += cur;
        }
        return sum;
    }
}