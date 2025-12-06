using ProjectCore;
using UnityEngine;
using UnityEngine.Pool;

namespace ProjectGame
{
    internal class CharactersPool: SceneSingleton<CharactersPool>
    {
        // The pool holds plain GameObjects (you can swap this for any component type).
        public ObjectPool<Character> Pool;

        [SerializeField] private Character _character;

        protected override void OnSingletonInit()
        {
            // Create a pool with the four core callbacks.
            Pool = new ObjectPool<Character>(
                createFunc: CreateItem,
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroyItem,
                collectionCheck: true,   // helps catch double-release mistakes
                defaultCapacity: 3,
                maxSize: 5
            );
        }

        // Creates a new pooled GameObject the first time (and whenever the pool needs more).
        private Character CreateItem()
        {
            var character = Instantiate(_character);
            character.name = "Character";
            character.gameObject.SetActive(false);
            return character;
        }

        // Called when an item is taken from the pool.
        private void OnGet(Character character)
        {
            character.gameObject.SetActive(true);
        }

        // Called when an item is returned to the pool.
        private void OnRelease(Character character)
        {
            character.gameObject.SetActive(false);
        }

        // Called when the pool decides to destroy an item (e.g., above max size).
        private void OnDestroyItem(Character character)
        {
            Destroy(character.gameObject);
        }
    }
}
