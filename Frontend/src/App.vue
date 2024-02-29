<template>
  <nav class="#nav">
    <ul>       
      <li><a class="active" href="/">
        <svg width="2.5rem" height="2.5rem" xmlns="http://www.w3.org/2000/svg">
            <path d="M2.568,7.179H8.96c1.411,0,2.557-1.145,2.557-2.557c0-1.412-1.146-2.557-2.557-2.557H8.534c-0.235,0-0.426,0.19-0.426,0.426c0,0.236,0.191,0.426,0.426,0.426H8.96c0.941,0,1.704,0.763,1.704,1.705S9.901,6.327,8.96,6.327H2.568c-0.236,0-0.426,0.19-0.426,0.426C2.142,6.988,2.333,7.179,2.568,7.179 M15.778,7.179c0-0.941-0.763-1.704-1.704-1.704h-0.427c-0.235,0-0.426,0.19-0.426,0.426c0,0.235,0.19,0.426,0.426,0.426h0.427c0.47,0,0.852,0.382,0.852,0.852c0,0.471-0.382,0.853-0.852,0.853H0.864c-0.236,0-0.426,0.19-0.426,0.426c0,0.235,0.19,0.426,0.426,0.426h13.21C15.016,8.884,15.778,8.12,15.778,7.179 M16.631,9.736H2.568c-0.236,0-0.426,0.19-0.426,0.426c0,0.236,0.19,0.426,0.426,0.426h14.062c0.94,0,1.704,0.764,1.704,1.705s-0.764,1.704-1.704,1.704h-0.427c-0.235,0-0.426,0.19-0.426,0.427c0,0.235,0.19,0.426,0.426,0.426h0.427c1.411,0,2.557-1.145,2.557-2.557S18.042,9.736,16.631,9.736 M10.665,11.44H4.273c-0.236,0-0.426,0.19-0.426,0.426c0,0.236,0.19,0.427,0.426,0.427h6.392c1.412,0,2.557,1.145,2.557,2.557s-1.146,2.557-2.557,2.557h-0.426c-0.236,0-0.426,0.19-0.426,0.426s0.19,0.427,0.426,0.427h0.426c1.883,0,3.41-1.526,3.41-3.409S12.548,11.44,10.665,11.44"
            fill="white"
            stroke="white"
            stroke-width="0"
            transform="scale(2)"
            ></path>
        </svg>
      </a></li>
      <li><a class="active" href="/calculator">
        <svg width="2.5rem" height="2.5rem" xmlns="http://www.w3.org/2000/svg">
            <path d="M17.431,2.156h-3.715c-0.228,0-0.413,0.186-0.413,0.413v6.973h-2.89V6.687c0-0.229-0.186-0.413-0.413-0.413H6.285c-0.228,0-0.413,0.184-0.413,0.413v6.388H2.569c-0.227,0-0.413,0.187-0.413,0.413v3.942c0,0.228,0.186,0.413,0.413,0.413h14.862c0.228,0,0.413-0.186,0.413-0.413V2.569C17.844,2.342,17.658,2.156,17.431,2.156 M5.872,17.019h-2.89v-3.117h2.89V17.019zM9.587,17.019h-2.89V7.1h2.89V17.019z M13.303,17.019h-2.89v-6.651h2.89V17.019z M17.019,17.019h-2.891V2.982h2.891V17.019z"
            fill="white"
            stroke="white"
            stroke-width="0"
            transform="scale(2)"
            ></path>
        </svg>
      </a></li>
      <li><a class="active" href="/faq">
        <svg width="2.5rem" height="2.5rem" xmlns="http://www.w3.org/2000/svg">
        <path d="M10,2.262c-3.486,0-6.322,2.837-6.322,6.322c0,2.129,1.105,4.126,2.905,5.291l0.009,3.396c0.002,0.168,0.093,0.326,0.24,0.406c0.072,0.041,0.149,0.061,0.228,0.061c0.086,0,0.171-0.023,0.246-0.07l6.338-3.922c0.037-0.021,0.069-0.049,0.098-0.08c1.618-1.193,2.581-3.084,2.581-5.082C16.322,5.099,13.485,2.262,10,2.262z M13.109,12.969c-0.016,0.01-0.03,0.023-0.044,0.037l-5.542,3.426l-0.006-2.594c0.012-0.027,0.023-0.057,0.03-0.086c0.05-0.203-0.041-0.414-0.221-0.52c-1.675-0.963-2.715-2.746-2.715-4.648c0-2.971,2.417-5.387,5.388-5.387c2.971,0,5.387,2.417,5.387,5.387C15.387,10.316,14.536,11.955,13.109,12.969z"
            fill="white"
            stroke="white"
            stroke-width="0.1"
            transform="scale(2)"
            ></path> 
        </svg>     
      </a></li>
    </ul>

  </nav>
 
  <div class="top-info">
    <div class="info-container">
        <div class="info-box-b1">
          <span class="left-word">Days smoking</span>
          <span class="right-word">{{ getAllUserDataValueByIndex(0, 'TotalNumberOfDays') + getAllUserDataValueByIndex(1, 'TotalNumberOfDays')}}</span>
        </div>
        <div class="info-box-red">
          <span class="left-word">Total spent (SEK)</span>
          <span class="right-word">{{ numberWithCommas(getAllUserDataValueByIndex(0, 'TotalSpent') + getAllUserDataValueByIndex(1, 'TotalSpent') ) }}</span>
        </div>
        <div class="info-box-b2">
          <span class="left-word">Amount of cigarettes</span>
          <span class="right-word">{{ numberWithCommas(Math.round(getAllUserDataValueByIndex(0, 'TotalQuantity') * getAllUserDataValueByIndex(0, 'TotalNumberOfDays')/7 * 20 + getAllUserDataValueByIndex(1, 'TotalQuantity') * getAllUserDataValueByIndex(1, 'TotalNumberOfDays')/7)) }}</span>
        </div>
        <div class="info-box-green">
          <span class="left-word">Total if invested (SEK)</span>
          <span class="right-word">{{ numberWithCommas(getAllUserDataValueByIndex(0, 'TotalIfInvested') + getAllUserDataValueByIndex(1, 'TotalIfInvested') ) }}</span>
        </div>
        <div class="info-box-b3">
          <span class="left-word">Total life lost in seconds</span>
          <span class="right-word">{{ numberWithCommas(Math.round(getAllUserDataValueByIndex(0, 'TotalQuantity') * getAllUserDataValueByIndex(0, 'TotalNumberOfDays')/7 * 20 * 11 * 60)) }} </span>
        </div>
        <div class="progress-container">
          <div class="progress-bar">
            <!-- TODO - REMOVE VALUE ADJUSTMENT TO PROGRESS BAR -->
            <div class="progress" :style="{ width: (getAllUserDataValueByIndex(0, 'TotalSpent') + getAllUserDataValueByIndex(1, 'TotalSpent')) * 10e4/(3.5*10e9) + '%' }">
              {{ Math.round((getAllUserDataValueByIndex(0, 'TotalSpent') + getAllUserDataValueByIndex(1, 'TotalSpent')) * 10e4/(3.5*10e9)) }}%
            </div>
          </div>
          <div class="progress-description">
            <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.9rem; font-style: italic;">If all users were to invest their funds in Knowit AB, valued at 3.5 billion SEK.</p>
          </div>
        </div>
    </div>
  </div>

  <router-view></router-view>
</template>


<script>
import axios from 'axios';
const URL_FOR_LOCAL_HOST = "https://localhost:7197/"

export default{
  data() {
    return{
      allUserData: []
    }
  }, 
  methods: {
    async getAllCalulatedUserData(){
      await axios.get(URL_FOR_LOCAL_HOST + "api/TodoApp/GetAllUsersCalculatedData")
      .then((Response)=>{ this.allUserData = Object.keys(Response.data).map(key => Response.data[key]);
        }
      )
    },
    numberWithCommas(x) {
      return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    },
    getAllUserDataValueByIndex(index, key) {
        const userDataItem = this.allUserData[index];
        return userDataItem ? userDataItem[key] : 0;
    }
  },
  mounted:function(){
        this.getAllCalulatedUserData()
      }
}
</script>

<style>

* {
  margin: 0;
  padding: 0;
}

body {
  font-family: 'Helvetica'
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;
  color: #2c3e50;
}

.navbar {
  display: flex;
  align-items: center; /* Center vertically */
  /* You can add other styles for your navbar here */
}

.icon-container {
  display: flex;
  justify-content: center; /* Center horizontally */
  align-items: center; /* Center vertically */
  margin-right: 1rem;
}

nav a {
  font-weight: bold;
  color: #752d2d;
}

nav a.router-link-exact-active {
  padding: 2rem;
  color: #42b983;
}

ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color:#303030;
  box-shadow: 0px 2px 7px 1px #000000;
}

li {
  float: left;
  margin-left: 1rem;
  margin-right: 1rem;
}

li a {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

li a:hover svg path {
  stroke: rgb(162, 177, 26);
  fill: rgb(162, 177, 26);
}

.home{
  padding: 1rem;
}

.card-graph{
  background-color: #d3d4d6;
  padding: 1rem;
  border-radius: 1rem;
  height: auto;
  margin: 1rem;
  box-shadow: 0px 1px 3px 1px #00000086;
  max-width: 70rem;
  /* margin-right: auto; */
}

.card-input{
  background-color: #d3d4d6;
  padding: 1rem;
  border-radius: 1rem;
  height: auto;
  margin: 1rem;
  box-shadow: 0px 1px 3px 1px #00000086;
  max-width: 25rem;
  /* margin-left: auto;  */
}

.column25 {
  float: left;
  width: 25%;
  max-width: 25rem;
}

.column75 {
  float: left;
  width: 75%;
  max-width: 70rem;
}

/* Clear floats after the columns */
.row:after {
  content: "";
  display: table;
  clear: both;
}

.info-container {
    display: grid;
    grid-template-columns: repeat(2,1fr);
    grid-gap: 1rem;
    justify-content: space-between; 
    background-color: rgb(255, 255, 255); 
    padding: 0.5rem; 
    border: 1px solid #ccc; 
    border-radius: 0.5rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.4);
    margin-top: 0.5rem;
}

.info-box{
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
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box .left-word {
    margin-right: 0px;
    font-weight: bold;  
    
}

.info-box .right-word {
    font-weight: bold; 
}

.info-box-red{
    background-color: rgb(224, 142, 121);
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box-red .left-word {
    margin-right: 0px;
    font-weight: bold;  
}

.info-box-red .right-word {
    font-weight: bold; 
}

.info-box-green{
    background-color: rgb(168, 235, 171);
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box-green .left-word {
    margin-right: 0px;
    font-weight: bold;    
}

.info-box-green .right-word {
    font-weight: bold; 
}

.info-box-b1{
    background-color: #887d69;
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box-b1 .left-word {
    margin-right: 0px;
    font-weight: bold;    
}

.info-box-b1 .right-word {
    font-weight: bold; 
}

.info-box-b2{
    background-color: #a39780;
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box-b2 .left-word {
    margin-right: 0px;
    font-weight: bold;    
}

.info-box-b2 .right-word {
    font-weight: bold; 
}

.info-box-b3{
    background-color: #c0b092;
    border-radius: 0.5rem;
    border-width: 0rem;
    color: rgb(0, 0, 0); 
    border-color: #555;
    padding: 0.5rem 0.5rem; 
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: auto; 
    margin-top: 0.1rem;
    margin-right: 0.5rem;
    margin-bottom: 0.1rem;
    box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.info-box-b3 .left-word {
    margin-right: 0px;
    font-weight: bold;    
}

.info-box-b3 .right-word {
    font-weight: bold; 
}

.top-info{
  background: #4e4e4e;
  box-shadow: 0px 2px 3px rgba(0, 0, 0, 0.685);
  padding: 1rem;
}

.progress-container {
  text-align: center;
}

.progress-bar {
  width: 99%;
  background-color: #f0f0f0;
  height: 20px;
  border-radius: 4px;
  overflow: hidden;
  box-shadow: 0px 1px 3px rgba(0, 0, 0, 0.3);
}

.progress {
  height: 100%;
  background-color: #b3c2d3; /* Progress bar color */
  color: #fff; /* Text color */
  display: flex;
  align-items: center;
  justify-content: center;
  transition: width 0.5s ease-in-out;
  max-width: 100%;
}

.progress-description {
  margin-top: 5px; /* Add margin between progress bar and description */
}


</style>
