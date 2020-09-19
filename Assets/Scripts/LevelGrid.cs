﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid 
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int w, int h){
        this.width = w;
        this.height = h;
    }

    public void Setup(Snake snake){
        this.snake = snake;
        SpawnFood();
    }

    private void SpawnFood(){
        do{
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }while(snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        int randomFoodSpriteInt = Random.Range(0, GameAssets.instance.foodSpriteArray.Length);
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSpriteArray[randomFoodSpriteInt];
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }

    public bool DidSnakeEatFood(Vector2Int snakeGridPosition){
        if(snakeGridPosition == foodGridPosition){
            Object.Destroy(foodGameObject);
            SpawnFood();
            Score.AddScore();
            return true;
        }
        return false;

    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition){
        if(gridPosition.x < 0){
            gridPosition.x = width - 1;
        }
        if(gridPosition.y < 0){
            gridPosition.y = height - 1;
        }
        if(gridPosition.x > width - 1){
            gridPosition.x = 0;
        }
        if(gridPosition.y > height - 1){
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
