using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
//using Firebase
//using Firebase.Storage.FirebaseStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebase_DatabaseReference : MonoBehaviour
{
    void Start()
    {
        Firebase.Storage.FirebaseStorage storage = Firebase.Storage.FirebaseStorage.DefaultInstance;

        //Points to the root reference
        Firebase.Storage.StorageReference storage_ref =
          storage.GetReferenceFromUrl("gs://mapproject-fb612.appspot.com");


        //var new_metadata = new Firebase.Storage.MetadataChange
        //{
        //    CustomMetadata = new Dictionary<string, string> {
        //         {"className", "Foundations of Software Engineering"},
        //         {"classLocation", "Poly PICHO 150"}
        //    }
        //};

        // UpdateMetadataAsync

        //var new_metadata = new Firebase.Storage.MetadataChange
        //{
        //    LocationMetadata = new Dictionary<string, string> {
        //        {"className", "Foundations of Software Engineering"},
        //        {"classLocation", "Poly PICHO 150"}
        //    }
        //};

        // UpdateMetadataAsync

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


}
