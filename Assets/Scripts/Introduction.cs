using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{
    [SerializeField] private GameObject[] images;
    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        ShowImage(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextImage();
        }
    }

    private void ShowImage(int index)
    {
        for (int i = 0; i < images.Length; i++)
            images[i].SetActive(i == index);
    }
    private void NextImage()
    {
        currentIndex++;

        if (currentIndex >= images.Length)
        {
            SceneManager.LoadScene("GameLevel"); //Q pase al juego directamente
        }
        ShowImage(currentIndex);
    }
    //La última imagen (en blanco) te enseñaria las mecanicas e instrucciones.
}
