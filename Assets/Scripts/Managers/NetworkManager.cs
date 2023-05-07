using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using Firebase;
using Firebase.Database;

public class NetworkManager : MonoBehaviour, IBootingComponent
{
    DatabaseReference DBreference;

    private const string masterPath_Rank = "rankInfo";
    public const string path_ID = "userID";
    public const string path_Score = "score";
    public const int MAX_RANK_COUNT = 20;
    private long rank = 0;

    private FirebaseApp firebaseInstance = null;

    private int dependencyCheckCount = 0;

    // 임시
    // 나중에 로그인 정보를 가져와서 아이디 가져오기 
    string myID = "test_ID";

#region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
#endregion

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            CheckFirebaseDependency();
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {

        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            isReady = true;
        }
        else
        {
            DBreference = FirebaseDatabase.DefaultInstance.RootReference;
            isReady = true;
        }
    }

    private void CheckFirebaseDependency()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseInstance = FirebaseApp.DefaultInstance;
                DBreference = FirebaseDatabase.DefaultInstance.RootReference;
                isReady = true;
            }
            else
            {
                Debug.LogError(
                    $"Could not resolve all Firebase dependencies: {dependencyStatus}");

                dependencyCheckCount++;

                if (dependencyCheckCount > 10) return;
                Debug.Log($"Retrying Firebase Depedency Check : {dependencyCheckCount}");
                CheckFirebaseDependency();
            }
        });
    }

    private void Start()
    {
        //long rand = Random.Range(0, 100000);
        //User_Rank rankInfo = new User_Rank(myID, 1000);
        //string json = JsonUtility.ToJson(rankInfo);

        //var value = await GetRankDB();

        //var db = await DBreference.Child(masterPath_Rank).GetValueAsync();
        //DBreference.Child(masterPath_Rank).OrderByChild(path_Score);
        //int i = 0;

        //UpdateScoreDB("test5", 5, 1);
        //UpdateScoreDB("test4", 4, 2);
        //UpdateScoreDB("test3", 3, 3);
        //UpdateScoreDB("test2", 2, 4);

        //UpdateScoreDB("test", 5, 0);
    }


    /// <summary>
    /// 위험 !!!
    /// 강제 수정 시 사용
    /// 동시 접근 시 데이터가 손상될 수 있음
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    /// <param name="_"></param>
    public void UpdateScoreDB(string id, long score, int rank)
    {
        if(rank < 1 || rank > MAX_RANK_COUNT)
        {
            Debug.Log($"Invalid Rank Input : {rank}");
            return;
        }
        Dictionary<string, object> childUpdate = new();
        childUpdate[$"/{rank}/" + path_ID] = id;
        childUpdate[$"/{rank}/" + path_Score] = score;
        
        DBreference.Child(masterPath_Rank).UpdateChildrenAsync(childUpdate);
    }


    public void UpdateScoreDB(string id, long newScore)
    {
        // 랭킹이 추가되면 나머지를 뒤로 밀어내야함
        DBreference.Child(masterPath_Rank).RunTransaction(data =>
        {
            List<object> leaders = data.Value as List<object>;

            Dictionary<string, object> dic = new();

            if (leaders == null)
                leaders = new List<object>();

            long minScore = long.MaxValue;
            object minLeader = null;

            // 점수 비교
            if (data.ChildrenCount >= MAX_RANK_COUNT)
            {
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;

                    long childScore = (long)((Dictionary<string, object>)child)[path_Score];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minLeader = child;
                    }
                }

                //새로운 점수가 최소 점수보다 낮음
                if (minScore > newScore)
                {
                    return TransactionResult.Abort();
                }
                //원래 최소 점수 제거
                leaders.Remove(minLeader);
            }
            dic[path_ID] = id;
            dic[path_Score] = newScore;

            leaders.Add(dic);
            data.Value = leaders;

            return TransactionResult.Success(data);
        });
    }

    public async Task<List<IDictionary>> GetRankDB()
    {
        if (DBreference == null) return null;

        List<IDictionary> results = null;
        var newTask = Task.Run(() => DBreference.GetValueAsync());
        var value = await newTask;

        if (newTask.IsCompleted)
        {
            DataSnapshot rankInfo = newTask.Result;

            int childCount = (int)rankInfo.Child(masterPath_Rank).ChildrenCount;
            results = new List<IDictionary>(childCount);
            for (int i = 0; i < childCount; i++)
            {
                var rank = rankInfo.Child(masterPath_Rank).Child(i.ToString());
                IDictionary info = (IDictionary)rank.Value;
                results.Add(info);
            }

            results = results.SortListIDictionary();
        }
        else if (newTask.IsFaulted)
        {
            Debug.LogError("랭킹 읽어오기 실패 : " + newTask.Exception);
        }
        return results;
    }

    private bool IsUserExist(string id)
    {
        bool result = false;

        DBreference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                var data = task.Result;

                for (int i = 0; i < data.Child(masterPath_Rank).ChildrenCount; i++)
                {
                    if (data.Child(masterPath_Rank).Child(i.ToString()).Child(path_ID).Value.Equals(id))
                    {
                        result = true;
                    }
                }
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("유저 검색 실패 : " + task.Exception);
            }
        });

        return result;
    }
}