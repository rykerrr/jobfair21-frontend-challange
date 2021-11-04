using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class animates all token instances in a scene.
    /// This allows a single update call to animate hundreds of sprite 
    /// animations.
    /// If the tokens property is empty, it will automatically find and load 
    /// all token instances in the scene at runtime.
    /// </summary>
    public class TokenController : MonoBehaviour
    {
        [Header("Preferences")]
        [Tooltip("Frames per second at which tokens are animated.")]
        [SerializeField] private float frameRate = 12;
        [SerializeField] private Transform tokensParent = default;
        [Tooltip("Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.")]
        [SerializeField] private List<TokenInstance> tokens = new List<TokenInstance>();
        
        float nextFrameTime = 0;

        [ContextMenu("Find All Tokens")]
        private void ResetTokensAndFindAllTokensInScene()
        {
            tokens.Clear();
            
            foreach (TokenInstance token in tokensParent)
            {
                tokens.Add(token);
            }
        }

        private void Awake()
        {
            //if tokens are empty, find all instances.
            //if tokens are not empty, they've been added at editor time.
            if (tokens.Count == 0)
                ResetTokensAndFindAllTokensInScene();
            //Register all tokens so they can work with this controller.
            for (var i = 0; i < tokens.Count; i++)
            {
                tokens[i].tokenIndex = i;
                tokens[i].controller = this;
            }
        }

        private void Update()
        {
            //if it's time for the next frame...
            if (Time.time - nextFrameTime > (1f / frameRate))
            {
                //update all tokens with the next animation frame.
                for (var i = 0; i < tokens.Count; i++)
                {
                    var token = tokens[i];
                    //if token is null, it has been disabled and is no longer animated.
                    if (token != null)
                    {
                        token.SpriteRenderer.sprite = token.sprites[token.frame];
                        if (token.Collected && token.frame == token.sprites.Length - 1)
                        {
                            token.gameObject.SetActive(false);
                            tokens[i] = null;
                        }
                        else
                        {
                            token.frame = (token.frame + 1) % token.sprites.Length;
                        }
                    }
                }

                //calculate the time of the next frame.
                nextFrameTime += 1f / frameRate;
            }
        }
    }
}