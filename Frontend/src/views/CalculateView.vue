<script setup>
import { Collapse } from 'vue-collapsed'
</script>

<template>
  <div class="row">
    <div class="column25">
      <div class="card-input">
        <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;">Start</p>
        <VueDatePicker
          v-model="startDate"
          month-picker
          :max-date="new Date(endDate.year, endDate.month)"
        ></VueDatePicker>
        <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;"> <br>End</p>
        <VueDatePicker
          v-model="endDate"
          month-picker
          :min-date="new Date(startDate.year, startDate.month)"
        ></VueDatePicker>

        <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;"> <br>Quantity per week</p>
          <div class="quantity-toggle">
            <button class="quantity-button" @click="quantity--">-</button> 
            {{ quantity }} 
            <button class="quantity-button" @click="quantity++">+</button> 
            <a> &nbsp; </a>
            <label class="toggle">
              <input type="checkbox" @click="toggleCheckbox">
              <span class="slider"></span>
              <span class="labels" data-on="Electronic" data-off="Tobacco"></span>
            </label> 
          </div>
        
          <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;"> <br>Price per unit (SEK)</p>

          <div class="quantity-toggle">
            <button class="quantity-button" @click="pricePerUnit--">-</button> 
            <input class="transparent-input" v-model="pricePerUnit" placeholder="Enter price per unit" /> 
            <button class="quantity-button" @click="pricePerUnit++">+</button> 
           
          </div>
          <div class="calculate-button-div">
            <button class="calculate-button" @click="getCalculatedUserData">Calculate</button>
          </div>
         
      </div>
    </div>
    <div class="column75">
      <div class="card-graph">
        <div class="bold-container-red">
          <b class="toggle-description-text" style="color: #000000;;font-size:1rem; font-style: ;"> Total money spent: <br> 
            <a class="toggle-description-text not-bold" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;">calculated based on a yearly inflation rate of 3% </a>
          </b>
          <b> {{ Math.round(parseFloat(getUserDataValueByCompound('historicalSpendings'))) }} SEK</b>
        </div>
        <div class="bold-container-green">
          <b class="toggle-description-text" style="color:#000000;font-size:1rem;"> Total money if invested: <br> 
            <a class="toggle-description-text not-bold" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;">calculated based on an annual return rate of 5%</a>
          </b>
          <b> {{ Math.round(parseFloat(getUserDataValueByCompound('totalIfInvested'))) }} SEK</b>
        </div>
        <div class="bold-container-white">
          <b class="toggle-description-text" style="color:#000000;font-size:1rem;"> Total life lost: <br> 
            <a class="toggle-description-text not-bold" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;">one cigarette reduces life by 11 minutes </a>
          </b>
          <b>{{ parseFloat(getUserDataValueByCompound('totalLifeLost')) }} sec</b>
        </div>
        <div v-if="!checkbox" class="bold-container-white">
          <b class="toggle-description-text" style="color:#000000;font-size:1rem;"> Total cigarettes smoked: </b>
          <b>{{ parseFloat(getUserDataValueByCompound('totalCigarettes'))}} </b>
        </div>
        <div v-if="!checkbox" class="bold-container-white">
          <b class="toggle-description-text" style="color:#000000;font-size:1rem;"> Average cigarettes per day: </b>
          <b>{{ (parseFloat(getUserDataValueByCompound('totalCigarettes'))/numberOfDays).toFixed(1) }} </b>
        </div>
        <div v-if="checkbox" class="bold-container-white">
          <b class="toggle-description-text" style="color:#000000;font-size:1rem;"> Total E-cigarettes smoked: </b>
          <b>{{ (parseFloat(getUserDataValueByCompound('totalCigarettes'))/20).toFixed(1) }} </b>
        </div>

        <p v-if="this.userData.length != 0" class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;"> <br>Click on each compound to display more information</p>
        <div v-for="(item, index) in this.userData" :key="index">
          <button v-if="index < this.userData.length - 5" class="toxin-button" @click="() => handleMultiple(index)">
            <span class="left-word">{{ item.Compound }}</span>
            <span class="right-word">{{ convertToUnit(item.totalConsumed) }}</span>
          </button>
          <Collapse :when="userData[index].isExpanded" class="collapse">
            <div class="description-toxin">
              <p>
              Add comparison with product containing compound {{ item.totalConsumed }}
              </p>
            </div>
          </Collapse>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import '../components/toggleSwitch.css'
import '../components/quantity.css'
import { reactive } from 'vue'

import axios from 'axios';
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

const URL_FOR_LOCAL_HOST = "https://localhost:7197/"



export default {
  name: 'CalculateView',
  components: { VueDatePicker },
  data() {
    return {
      checkbox: false,
      toxins: [],
      toxinLabels: [],
      tobaccoData: [],
      electronicData: [],
      startDate: {
        month: new Date().getMonth(),
        year: new Date().getFullYear(),
      },
      endDate: {
        month: new Date().getMonth(),
        year: new Date().getFullYear(),
      },
      numberOfDays: 0,
      futureDays: 0,
      quantity: 1,
      userData: reactive([]),
      pricePerUnit: null,
    }
  },

  methods: {
    async getToxinsData(){
      await axios.get(URL_FOR_LOCAL_HOST + "api/TodoApp/GetToxinData")
      .then((Response)=>{ this.toxins = Object.keys(Response.data).map(key => Response.data[key]);
        }
      )

      this.toxinlabels = this.toxins.map(item => item.Compound);
      this.tobaccoData = this.toxins.map(item => parseFloat(item["Tobacco"]));
      this.electronicData = this.toxins.map(item => parseFloat(item["Electronic"]));

    },
    toggleCheckbox() {
      this.checkbox = !this.checkbox
      this.$emit('setCheckboxVal', this.checkbox)
    },
    daysBetweenMonths() {
      const date1 = new Date(this.startDate.year, this.startDate.month - 1, 1);
      const date2 = new Date(this.endDate.year, this.endDate.month - 1, 1);
      const timeDifference = date2 - date1;

      const daysDifference = timeDifference / (1000 * 60 * 60 * 24);

      this.numberOfDays = Math.abs(Math.round(daysDifference));
    },
    calculateFutureDays(){
      const date1 = new Date( new Date().getFullYear(), new Date().getMonth() - 1, 1);
      const date2 = new Date(this.endDate.year, this.endDate.month - 1, 1);
      const timeDifference = date2 - date1;

      const daysDifference = timeDifference / (1000 * 60 * 60 * 24);

      this.futureDays = Math.abs(Math.round(daysDifference));
      
    },
    increment () {
      this.quantity++
    },
    decrement () {
      if(this.quantity === 1) {
        alert('Negative quantity not allowed')
      } else {
        this.quantity--
      }
    },
    async postUserData(){
      const formData = new FormData();
      const smokeType = this.checkbox ? 'Electronic' : 'Tobacco';
      formData.append("NumberOfDays", this.numberOfDays)
      formData.append("Type", smokeType)
      formData.append("Quantity", this.quantity)
      formData.append("TotalSpent", Math.round(parseFloat(this.getUserDataValueByCompound('historicalSpendings'))))
      formData.append("TotalIfInvested", Math.round(parseFloat(this.getUserDataValueByCompound('totalIfInvested'))))
      
      await axios.post(URL_FOR_LOCAL_HOST + "api/TodoApp/AddUser", formData)
      .then((Response)=>{ alert(Response.data);
        }
      )
    },
    async getCalculatedUserData(){
      this.daysBetweenMonths()
      this.calculateFutureDays()
      const smokeType = this.checkbox ? 'Electronic' : 'Tobacco';
      const getURL = "api/TodoApp/GetUserData" + this.numberOfDays.toString() + 
      '/' + smokeType + '/' + this.quantity.toString() + '/' 
      + this.pricePerUnit.toString() + '/' + this.futureDays.toString();
      
      await axios.get(URL_FOR_LOCAL_HOST + getURL)
      .then((Response)=>{ 
        this.userData = Object.keys(Response.data).map(key => 
        {
          const item = Response.data[key];
          item.isExpandable = false; 
          return item;
        });
      }
      )
      this.postUserData()

    },
    handleMultiple(index) {
        this.userData[index].isExpanded = !this.userData[index].isExpanded
    },
    getUserDataValueByCompound(compound) {
        const userDataItem = this.userData.find(item => item.Compound === compound);
        return userDataItem ? userDataItem.totalConsumed : 0;
    },
    convertToUnit(value) {
      if (value >= 1000 * 1000) {
        return (value / (1000 * 1000)).toFixed(2) + " kg";
      } else if (value >= 1000) {
        return (value / 1000).toFixed(2) + " g";
      } else {
        return value.toFixed(2) + " mg";
      }
    }
  },

  mounted:function(){
        this.getToxinsData()
      }
}
</script>

<style>

.calculate-button-div{
  display: flex;

}

.calculate-button{
  padding: 0.8rem  0.8rem;
  text-align: center;
  font-size: 0.8rem;
  text-transform: uppercase;
  cursor: pointer;
  background: #00000041;
  border-radius: 2rem;
  border: 0rem solid #1e1b1b;
  color: #ffffff;
  font-weight: bold;
  letter-spacing: 1px;
  margin-top: 2rem;
  margin-left: auto;
  transition: background-color 0.3s;
  box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.555);
}

.calculate-button:hover{
  background-color: #555;
}

.transparent-input {
  background-color: #ccc;
  border-radius: 0.5rem;
  border: none;
  outline: none; 
  width: 60%;
  text-align: center;
  height: 1.5rem; 
  padding: 0.1rem;
  color: #000;
  font-size: 1rem;
}

.toxin-button {
    background-color: #ffffff96;
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%; 
    margin-top: 0.5rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.toxin-button .left-word {
    margin-right: 0px; 
    
}

.toxin-button .right-word {
    font-weight: bold; 
}

.description-toxin {
  background-color: #b1b1b196;
  padding: 0.5rem;
}

.bold-container-red {
    display: flex;
    justify-content: space-between; 
    background-color: rgb(224, 142, 121); 
    padding: 0.5rem; 
    border: 1px solid #ccc; 
    border-radius: 0.5rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.4);
    margin-top: 0.5rem;
    
}

.bold-container-green {
    display: flex;
    justify-content: space-between;
    background-color: rgb(168, 235, 171); 
    padding: 0.5rem; 
    border: 1px solid #ccc; 
    border-radius: 0.5rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.4);
    margin-top: 0.5rem;
}

.bold-container-white {
    display: flex;
    justify-content: space-between; 
    background-color: rgb(255, 255, 255); 
    padding: 0.5rem; 
    border: 1px solid #ccc; 
    border-radius: 0.5rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.4);
    margin-top: 0.5rem;
}

.not-bold {
    font-weight: normal; 
}


</style>
  
