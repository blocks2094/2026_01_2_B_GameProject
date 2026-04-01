using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour
{
   public static DialogManager Instance {  get; private set; }

    [Header("Dialog Reterences")]
    [SerializeField] private DialogDatabaseSO dialogDatabase;

    [Header("UI References")]
    [SerializeField] private GameObject dialogPener;

    [SerializeField] private Image portraitImage;   // 캐릭터 초상화 이미지 추가
    


    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Button NextButton;

    [Header("Dialog Setting")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool useTyperriterEffect = true;

    private bool isTyping = false;
    private Coroutine typingCoroutine;
    

    private DialogSO currentDialog;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  

        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(dialogDatabase == null)
        {
            dialogDatabase.Initailize();    // 초기화
        }
        else
        {
            Debug.LogError("Dialog Database is noot assinged to Dialgo Manager");
        }

        if(NextButton != null)
        {
            //NextButton.onClick.AddListener(NextDialog);       // 버튼 리스너 등록
        }
        else
        {
            Debug.LogError("Next Button is Not assigned!");
        }
    }

    void Start()
    {
        // UI 초기화 후 대화 시작 (ID 1)
        CloseDialog();
        StartDialog(1); // 자동으로 첫번째 대화 시작
    }

    
    void Update()
    {
        
    }

    // ID로 대화 시작
    public void StartDialog(int dialogId)
    {
        DialogSO dialog = dialogDatabase.GetDialogByld(dialogId);
        if(dialog != null)
        {
            StartDialog(dialog);
        }
        else
        {
            Debug.LogError($"Dialog with ID {dialogId} not found");
        }
    }

    // Dialog로 대화 시작
    public void StartDialog(DialogSO dialog)
    {
        if (dialog == null) return;

        currentDialog = dialog;
        ShowDialog();
        dialogPener.SetActive(true);
    }

    public void ShowDialog()
    {
        if (currentDialog == null) return;
        characterNameText.text = currentDialog.characterName;   // 캐릭터 이름 설정
        

        if (useTyperriterEffect)
        {
            StartTypingEffect(currentDialog.text);
        }
        else
        {
            dialogText.text = currentDialog.text;   
        }

            

        // 초상화 설정
        if(currentDialog.portrait != null)
        {
            portraitImage.sprite = currentDialog.portrait;
            portraitImage.gameObject.SetActive(true);
        }
        else if(!string.IsNullOrEmpty(currentDialog.portraitPath)) 
        {
            // Resources 폴더에서 이미지 로드
            Sprite portrait = Resources.Load<Sprite>(currentDialog.portraitPath);
            if(portrait != null)
            {
                portraitImage.sprite = portrait;
                portraitImage.gameObject.SetActive(true);   
            }
            else
            {
                Debug.LogWarning($"Portait not found at path : {currentDialog.portraitPath}");
                portraitImage.gameObject.SetActive(false);
            }
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }
    }

    public void CloseDialog()   // 대화 종료
    {
        dialogPener.SetActive(false);
        currentDialog = null;
        StopTypingEffect();     // 타이핑 효과 중지
    }

    public void NextDialog()
    {
        if(isTyping)    // 타이핑 중이면 타이핑 완료 처리
        {
            StopTypingEffect();
            dialogText.text = currentDialog.text;
            isTyping = false;
            return;
        }

        if(currentDialog != null && currentDialog.nextId > 0)
        {
            DialogSO nextDialog = dialogDatabase.GetDialogByld(currentDialog.nextId);
            if (nextDialog != null)
            {
                currentDialog = nextDialog;
                ShowDialog();
            }
            else
            {
                CloseDialog();
            }
        }
        else
        {
            CloseDialog();
        }
    }


    // 텍스트 타이핑
    private IEnumerator TypeText(string type)
    {
        dialogText.text = "";
        foreach(char c in type)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void StopTypingEffect()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    // 타이핑 효과 함수 시작
    private void StartTypingEffect(string text)
    {
        isTyping = true;
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(text));
    }
}
