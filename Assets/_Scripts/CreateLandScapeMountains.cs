using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;


public class CreateLandScapeMountains : MonoBehaviour
{
    public Terrain terrain;
    public float[,] copy_heights;

    private int xBase = 0;
    private int yBase = 0;
    private int min = 0;
    private int max = 256;
    private int StartX;
    private int StartZ;
    private int borderX;
    private int borderZ;
    private float rafting;
    private int random_point_rafting;
    private static int percent_length_I;
    private static int percent_length_J;
    private float HeightEndValue;
    private float HeightStartValue;

    private bool flag = false;
    
    private float height = 0.0f;
    private int size_mountain;

    [Range(0.0005f,0.1f)]
    public float delta = 0.0005f;

    [Range(1,30)]
    public int offset = 1;

    [Range(1, 1000)]
    public int count = 1;

    [Range(1, 10)]
    public int count_mountains = 1;

    

    

    [Range(0.00001f, 0.0001f)]
    public float StartCoreCoeffRafting;

    [Range(0.0001f, 0.001f)]
    public float EndCoreCoeffRafting;

    [Range(0.1f, 1.0f)]
    public float percent_folding;

    void SetFolding()
    {
        percent_length_I = (int)(core_rafting.GetLength(0) * percent_folding) + 1;
        percent_length_J = (int)(core_rafting.GetLength(0) * percent_folding) + 1;
    }

    private static float[,] core_rafting;
    
    void allocate_core()
    {
        
        for (int i = 0; i < percent_length_I; i++)
        {
            
            for (int j = 0; j < percent_length_J; j++)
            {
                core_rafting[i,j] = Random.Range(StartCoreCoeffRafting,EndCoreCoeffRafting);
            }
        }
    }

    void Start()
    {

        terrain = GetComponent<Terrain>();
        copy_heights = terrain.terrainData.GetHeights(xBase, yBase, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
        EditTerrainHeight();
    }

    void OnDestroy()
    {
        ResetTerrainHeight();
    }

    void EditTerrainHeight()
    {
        
        TerrainData terrainData = terrain.terrainData;
        //Get array heights terrain
        float[,] heights = terrainData.GetHeights(xBase, yBase, terrainData.alphamapWidth, terrainData.alphamapHeight);
        for (int i = 0; i < count; i++)//возвышенности ниже гор
        {
            int size = Random.Range(15, 20);
            core_rafting = null;
            core_rafting = new float[size, size];
            allocate_core();
            SetFolding();
            StartX = Clamp(Random.Range(min, max));//random start coord x to draw
            borderX = StartX;
            StartZ = Clamp(Random.Range(min, max)); //random start coord z to draw
            borderZ = StartZ;
            for (StartX = borderX; StartX < Clamp(borderX + size); StartX++)
            {
                height = 0;
                for (StartZ = borderZ; StartZ < Clamp(borderZ + size); StartZ++)
                {
                    rafting = -delta * Random.Range(0.1f, 0.9f);

                    if (StartX >= borderX && StartX <= Clamp(borderX + size / 4))//1/4 size_mountain
                    {
                        random_point_rafting = Random.Range(Clamp(borderZ + size / 4), Clamp(borderZ + size / 2));

                        rafting = -delta * Random.Range(0.3f, 0.6f);
                        if (StartZ >= Clamp(borderZ + size / 4) && StartZ < Clamp(borderZ + size / 2))
                        {
                            if (random_point_rafting == StartZ)
                            {

                                for (int k = 0; k < percent_length_I; k++)
                                {

                                    for (int l = 0; l < percent_length_J; l++)
                                    {
                                        if (Clamp(StartX + l) >= 255 || Clamp(StartZ + k) >= 255) { break; }
                                        heights[Clamp(StartX + l), Clamp(StartZ + k)] += height + (core_rafting[k, l] * height);
                                    }
                                }


                            }
                            else
                            {
                                height += delta + rafting;


                                heights[Clamp(StartX), Clamp(StartZ)] += height;
                            }
                        }
                        else if (StartZ >= Clamp(borderZ + size / 2) && StartZ < (int)(Clamp(borderZ + size * 3 / 4)))
                        {

                            height -= delta + rafting;
                            heights[Clamp(StartX), Clamp(StartZ)] += height;

                        }
                    }

                    else if (StartX > Clamp(borderX + size / 4) && StartX <= Clamp(borderX + size / 2))//2/4 size_mountain
                    {
                        random_point_rafting = Random.Range(Clamp(borderZ + size / 4), Clamp(borderZ + size / 2));
                        rafting = -delta * (0.65f) * Random.Range(0.1f, 0.6f);
                        if (StartZ >= Clamp(borderZ + size / 4) && StartZ < (int)(Clamp(borderZ + size / 2)))
                        {
                            if (random_point_rafting == StartZ)
                            {

                                for (int k = 0; k < percent_length_I; k++)
                                {

                                    for (int l = 0; l < percent_length_J; l++)
                                    {
                                        if (Clamp(StartX + l) >= 255 || Clamp(StartZ + k) >= 255) { break; }
                                        heights[Clamp(StartX + l), Clamp(StartZ + k)] += height + (core_rafting[k, l] * height);
                                    }
                                }


                            }
                            else
                            {
                                height += delta * (0.65f) + rafting;


                                heights[Clamp(StartX), Clamp(StartZ)] += height;
                            }

                        }
                        else if (StartZ >= Clamp(borderZ + size / 2) && StartZ < (int)(Clamp(borderZ + size * 3 / 4)))
                        {

                            height -= delta + rafting;
                            heights[Clamp(StartX), Clamp(StartZ)] += height;
                        }
                    }
                    //else if (StartX > Clamp(borderX + size_mountain / 2) && StartX <= (int)(Clamp(borderX + size_mountain) * 3 / 4))//3/4 size_mountain
                    //{
                    //    if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ <= (int)(Clamp(borderZ + size_mountain / 2)))
                    //    {
                    //        if (layer3 == false)
                    //        {
                    //            if (StartZ == Clamp(borderZ + size_mountain / 4))
                    //            {
                    //                height = sum_height;
                    //            }
                    //            height += delta + rafting;
                    //            if (StartZ + 1 == (int)(Clamp(borderZ + size_mountain / 2)))
                    //            {
                    //                sum_height = height;
                    //                layer3 = true;
                    //            }
                    //        }
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //    else if (StartZ > Clamp(borderZ + size_mountain / 2) && StartZ <= (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                    //    {
                    //        height -= delta;
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //}
                    //else if (StartX > (int)(Clamp(borderX + size_mountain) * 3 / 4) && StartX <= Clamp(borderX + size_mountain))//4/4 size_mountain
                    //{
                    //    if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ <= (int)(Clamp(borderZ + size_mountain / 2)))
                    //    {
                    //        if (layer4 == false)
                    //        {
                    //            if (StartZ == Clamp(borderZ + size_mountain / 4))
                    //            {
                    //                height = sum_height;
                    //            }
                    //            height += delta + rafting;
                    //            if (StartZ + 1 == (int)(Clamp(borderZ + size_mountain / 2)))
                    //            {
                    //                sum_height = height;
                    //                layer4 = true;
                    //            }
                    //        }
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //    else if (StartZ > Clamp(borderZ + size_mountain / 2) && StartZ <= (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                    //    {
                    //        height -= delta;
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //}
                }
            }
        }




            for (int i = 0; i < count_mountains; i++)//горы
              {
            size_mountain = Random.Range(25, 55);
            core_rafting = null;
            core_rafting = new float[size_mountain, size_mountain];
            allocate_core();
            SetFolding();
            StartX = Clamp(Random.Range(min, max));//random start coord x to draw
            borderX = StartX;
            StartZ = Clamp(Random.Range(min, max)); //random start coord z to draw
            borderZ = StartZ;
            for (StartX = borderX; StartX < Clamp(borderX + size_mountain); StartX++)
            {
                height = 0;
                for (StartZ = borderZ; StartZ < Clamp(borderZ + size_mountain); StartZ++)
                {
                    rafting = -delta * Random.Range(0.1f, 0.9f);
                
                    if (StartX >= borderX && StartX <= Clamp(borderX + size_mountain / 4))//1/4 size_mountain
                    {
                        random_point_rafting = Random.Range(Clamp(borderZ + size_mountain / 4), Clamp(borderZ + size_mountain / 2));
                        
                        rafting = -delta * Random.Range(0.3f, 0.6f);
                        if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ < Clamp(borderZ + size_mountain / 2))
                        {
                            if (random_point_rafting == StartZ)
                            {
                                
                                    for (int k = 0; k < percent_length_I; k++)
                                    {

                                        for (int l = 0; l < percent_length_J; l++)
                                        {
                                            if (Clamp(StartX + l) >= 255 || Clamp(StartZ + k) >= 255) { break; }
                                            heights[Clamp(StartX + l), Clamp(StartZ + k)] += height + (core_rafting[k, l] * height);
                                        }
                                    }
                                
                                
                            }
                            else
                            {
                                height += delta + rafting;


                                heights[Clamp(StartX), Clamp(StartZ)] += height;
                            }
                        }
                        else if (StartZ >= Clamp(borderZ + size_mountain / 2) && StartZ < (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                        {
                            
                            height -= delta+rafting;
                            heights[Clamp(StartX), Clamp(StartZ)] += height;
                            
                        }
                    }

                    else if (StartX > Clamp(borderX + size_mountain / 4) && StartX <= Clamp(borderX + size_mountain / 2))//2/4 size_mountain
                    {
                        random_point_rafting = Random.Range(Clamp(borderZ + size_mountain / 4), Clamp(borderZ + size_mountain / 2));
                        rafting = -delta*(0.65f) * Random.Range(0.1f, 0.6f);
                        if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ < (int)(Clamp(borderZ + size_mountain / 2)))
                        {
                            if (random_point_rafting == StartZ)
                            {

                                for (int k = 0; k < percent_length_I; k++)
                                {

                                    for (int l = 0; l < percent_length_J; l++)
                                    {
                                        if (Clamp(StartX + l) >= 255 || Clamp(StartZ + k) >=255) { break; }
                                        heights[Clamp(StartX + l), Clamp(StartZ + k)] += height + (core_rafting[k, l] * height);
                                    }
                                }


                            }
                            else
                            {
                                height += delta * (0.65f) + rafting;


                                heights[Clamp(StartX), Clamp(StartZ)] += height;
                            }
                           
                        }
                        else if (StartZ >= Clamp(borderZ + size_mountain / 2) && StartZ < (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                        {
                            
                            height -= delta+rafting;
                            heights[Clamp(StartX), Clamp(StartZ)] += height;
                        }
                    }
                    //else if (StartX > Clamp(borderX + size_mountain / 2) && StartX <= (int)(Clamp(borderX + size_mountain) * 3 / 4))//3/4 size_mountain
                    //{
                    //    if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ <= (int)(Clamp(borderZ + size_mountain / 2)))
                    //    {
                    //        if (layer3 == false)
                    //        {
                    //            if (StartZ == Clamp(borderZ + size_mountain / 4))
                    //            {
                    //                height = sum_height;
                    //            }
                    //            height += delta + rafting;
                    //            if (StartZ + 1 == (int)(Clamp(borderZ + size_mountain / 2)))
                    //            {
                    //                sum_height = height;
                    //                layer3 = true;
                    //            }
                    //        }
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //    else if (StartZ > Clamp(borderZ + size_mountain / 2) && StartZ <= (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                    //    {
                    //        height -= delta;
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //}
                    //else if (StartX > (int)(Clamp(borderX + size_mountain) * 3 / 4) && StartX <= Clamp(borderX + size_mountain))//4/4 size_mountain
                    //{
                    //    if (StartZ >= Clamp(borderZ + size_mountain / 4) && StartZ <= (int)(Clamp(borderZ + size_mountain / 2)))
                    //    {
                    //        if (layer4 == false)
                    //        {
                    //            if (StartZ == Clamp(borderZ + size_mountain / 4))
                    //            {
                    //                height = sum_height;
                    //            }
                    //            height += delta + rafting;
                    //            if (StartZ + 1 == (int)(Clamp(borderZ + size_mountain / 2)))
                    //            {
                    //                sum_height = height;
                    //                layer4 = true;
                    //            }
                    //        }
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //    else if (StartZ > Clamp(borderZ + size_mountain / 2) && StartZ <= (int)(Clamp(borderZ + size_mountain * 3 / 4)))
                    //    {
                    //        height -= delta;
                    //        heights[Clamp(StartX + offsetX), Clamp(StartZ)] = height;
                    //    }
                    //}
                }
            }

            
          
            terrainData.SetHeights(xBase, yBase, heights);//SET HEIGHTS TERRAIN
        }
    }


    void ResetTerrainHeight()
    {
        terrain.terrainData.SetHeights(xBase, yBase, copy_heights);//RESET HEIGHTS
    }

    int Clamp(int value, int min = 0, int max = 255)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        return value;
    }
}
