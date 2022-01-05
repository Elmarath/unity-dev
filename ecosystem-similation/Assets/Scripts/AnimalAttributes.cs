public struct AnimalAttributes
{
    public float normalSpeed;
    public float waitTime;
    public float minSearchDistance;
    public float maxHunger;
    public float maxThirst;
    public float maxReproduceUrge;
    public float howManyChildren;
    public float gettingHungryRate;
    public float gettingThirstyRate;
    public float gettingHornyRate;
    public float pregnantTimeRate;
    public float gettingFullMultiplier;
    public float drinkingRate;
    public float becomeAdultTime;

    public void resetVariables()
    {
        normalSpeed = 0;
        waitTime = 0;
        minSearchDistance = 0;
        maxHunger = 0;
        maxThirst = 0;
        maxReproduceUrge = 0;
        howManyChildren = 0;
        gettingHungryRate = 0;
        gettingThirstyRate = 0;
        gettingHornyRate = 0;
        pregnantTimeRate = 0;
        gettingFullMultiplier = 0;
        drinkingRate = 0;
        becomeAdultTime = 0;
    }
}