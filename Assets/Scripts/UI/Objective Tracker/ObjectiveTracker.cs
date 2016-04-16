﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Sol
{
    public class ObjectiveTracker : Menu
    {
        public ObjectiveDisplay objectivePrefab;
        public Transform objectiveContainer;

        public Text objectiveTextAdmin;
        public Text objectiveTextAI;

        public List<ObjectiveDisplay> displayedObjectives = new List<ObjectiveDisplay>();


        public override void Open()
        {
            base.Open();
        }


        public void Open(string objective, bool admin = true)
        {
            if (admin)
            {
                objectiveTextAdmin.text = objective;
                objectiveTextAI.text = "";
            }
            else
            {
                objectiveTextAdmin.text = "";
                objectiveTextAI.text = objective;
            }

            Open();
        }


        /*public ObjectiveDisplay AddObjective(Objective objective, float displaySpeed = 0f)
        {
            if (!IsActive) Open();

            StopAllCoroutines();
            StartCoroutine(DelayedClose());

            ObjectiveDisplay od = Instantiate(objectivePrefab);
            od.transform.SetParent(objectiveContainer, false);
            od.objective = objective;
            od.Initialize(displaySpeed);
            displayedObjectives.Add(od);

            return od;
        }*/
    }

}