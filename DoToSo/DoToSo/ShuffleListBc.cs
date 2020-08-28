using System;
using System.Collections.Generic;

namespace DoToSo
{
    public class ShuffleListBc<TEntity>
    {
        public void FisherYatesShuffle(List<TEntity> listToShuffle, Random random)
        {
            for(int i = listToShuffle.Count - 1; i >= 0 ; i--)
            {
                int randomNumber = random.Next(i);
                TEntity randomListElement = listToShuffle[randomNumber];
                listToShuffle[randomNumber] = listToShuffle[i];
                listToShuffle[i] = randomListElement;
            }
        }
    }
}
