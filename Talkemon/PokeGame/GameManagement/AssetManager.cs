using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

public class AssetManager
{
    protected ContentManager contentManager;
    protected float volume;

    public void PlaySound(string assetName)
    {
        SoundEffect snd = contentManager.Load<SoundEffect>(assetName);
        snd.Play();
    }

    public void PlayMusic(string assetName, bool repeat = true)
    {
        //MediaPlayer.IsRepeating = repeat;
        //MediaPlayer.Play(contentManager.Load<Song>(assetName));
    }

    public AssetManager(ContentManager Content)
    {
        this.contentManager = Content;
    }

    public Texture2D GetSprite(string assetName)
    {
        if (assetName == "")
            return null;

        return contentManager.Load<Texture2D>(assetName);
    }

    public ContentManager Content
    {
        get { return contentManager; }
    }

    public float Volume
    {
        get { return MediaPlayer.Volume; }
        set { MediaPlayer.Volume = value; }
    }

}

