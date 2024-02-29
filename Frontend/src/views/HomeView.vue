<template>

  <div class="row">
    <div class="column25">
      <div class="card-input">
        <div class="custom-select">
          <select id="option" v-model="selectedOption" @change="highlightMatchingBar">
            <option value="selectCompound" disabled selected>Select a compound</option>
            <option v-for="(option, index) in toxinDescriptions" :key="index">
              {{ option[0] }} 
            </option>
          </select>
        </div>
        <div v-if="selectedOption != 'selectCompound'" class="description-box">
          <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.8rem; font-style: italic;"> Content displayed in units/puff <br> {{ getToxinContent(selectedOption) }} </p>
          <p>
            {{ getDescriptionForToxin(selectedOption) }}
          </p>
        </div>
        <div v-else class="description-box">
          <p>
            Select a compound from list above to see how it affects your body.
          </p>
        </div>
        <div>
          <p class="toggle-description-text" style="color:#7a7a7a;font-size:0.8rem; font-style: italic;">toggle to display different data</p>
          <label class="toggle">
            <input type="checkbox" @click="toggleCheckbox" @change="highlightMatchingBar">
            <span class="slider"></span>
            <span class="labels" data-on="Electronic" data-off="Tobacco"></span>
          </label>
          <label class="toggle">
            <input type="checkbox" @click="toggleCheckboxDisplayBoth" @change="highlightMatchingBar">
            <span class="slider"></span>
            <span class="labels" data-on="Both" data-off="Single"></span>
          </label>
        </div>
      </div>
    </div>
    <div class="column75">
      <div class="card-graph">
        <Bar
          ref="myChartRef"
          v-if="checkboxDisplayBoth"
          id="my-chart-id"
          :options="chartOptions"
          :data="chartData"
        />
        <Bar
          ref="myChartRef"
          v-else-if="checkbox"
          id="my-chart-id"
          :options="chartOptions"
          :data="chartDataElectronic"
        />
        <Bar
          ref="myChartRef"
          v-else-if="!checkbox"
          id="my-chart-id"
          :options="chartOptions"
          :data="chartDataTobacco"
        />

      </div>
    </div>

  </div>



</template>


<script>
import '../components/toggleSwitch.css'
import '../components/toxinsList.css'
import axios from 'axios';
import { Bar } from 'vue-chartjs'
import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale, LogarithmicScale } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale, LogarithmicScale)

const URL_FOR_LOCAL_HOST = "https://localhost:7197/"

export default {
  name: 'HomeView',
  components: { Bar },
  data() {
    return {
      toxins: [],
      toxinLabels: [],
      toxinDescriptions: [],
      selectedOption: "selectCompound",
      checkbox: false,
      checkboxDisplayBoth: false,
      chartData:{
        labels: [],
        datasets: [ { data: [] } ]
      }, 
      chartDataTobacco: {
        labels: [],
        datasets: [ { data: [] } ]
      },
      chartDataElectronic: {
        labels: [],
        datasets: [ { data: [] } ]
      },
      chartOptions: {
        borderWidth: 2,
        borderRadius: 5,
        borderSkipped: false,
        borderColor: '#ccc',
        hoverBorderColor: '#e41a1c',
        backgroundColor: '#ccc',
        pointHoverBackgroundColor: "#fff",
        lineTension: 0.5,
        responsive: true,
        plugins:{
          legend:{
            display: false
          }
        },
        scales: {
          yAxes: {
            display: true,
            beginAtZero: true,
            position: 'right',
            type: 'logarithmic',
            grid: {
              display: true, 
              drawBorder: false,
            },
            ticks: {
              maxTicksLimit: 15,
              callback: function (value) {
                if (Math.abs(value) < 1) {
                return Math.abs(value) < 0.01 ? value.toExponential(0) : value.toFixed(2);
              } else {
                return Math.round(value).toString();
              }
              },
            },
            padding: 10,
          },
          y: {
            display: false,
          },
          x: {
            display: false
          }
        }
      },
      allUserData: [],
      allDataToggle: false,
    }
  },

  methods: {
    async getData(){
      await axios.get(URL_FOR_LOCAL_HOST + "api/TodoApp/GetToxinData")
      .then((Response)=>{ this.toxins = Object.keys(Response.data).map(key => Response.data[key]);
        }
      )

      const labels = this.toxins.map(item => item.Compound);
      const concentrationsCigarette = this.toxins.map(item => parseFloat(item["Tobacco"]));
      const concentrationsECigarette = this.toxins.map(item => parseFloat(item["Electronic"]));
      this.toxinLabels = labels;

      this.chartData = {
        labels: labels,
        datasets: [
        {
          label: 'Tobacco',
          data: concentrationsCigarette
        },
        {
          label: 'Electronic',
          data: concentrationsECigarette
        }
        ]
      };
      this.chartDataTobacco = {
        labels: labels,
        datasets: [
        {
          label: 'Tobacco',
          data: concentrationsCigarette
        }
        ]
      };
      this.chartDataElectronic = {
        labels: labels,
        datasets: [
        {
          label: 'Electronic',
          data: concentrationsECigarette
        }
        ]
      };
    },
    toggleCheckbox() {
      this.checkbox = !this.checkbox
      this.$emit('setCheckboxVal', this.checkbox)
    },
    toggleCheckboxDisplayBoth() {
      this.checkboxDisplayBoth = !this.checkboxDisplayBoth
      this.$emit('setCheckboxVal', this.checkboxDisplayBoth)
    },
    async getOptionsData() {
      try {
        const response = await axios.get(URL_FOR_LOCAL_HOST + "api/TodoApp/GetToxinsDescriptions");
        this.toxinDescriptions = response.data.map(item => [item.toxin, item.description]);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    },
    getDescriptionForToxin(toxinName) {
      const foundOption = this.toxinDescriptions.find(option => option[0] === toxinName);

      if (foundOption) {
        return foundOption[1];
      } else {
        return "Toxin not found";
      }
    },
    getIndexByToxin(toxinName) {
      const foundIndex = this.toxins.findIndex(item => item.Compound === toxinName);
      if (foundIndex != -1) {
        return foundIndex;
      } else {
        return "Toxin not found";
      }
    },
    highlightMatchingBar() {
      const chart = this.$refs.myChartRef.chart;
      var searchValue = document.getElementById("option").value.toUpperCase();
      if (this.checkboxDisplayBoth){
        chart.data.datasets[0].backgroundColor = this.toxinLabels.map(
        (v) => v.toUpperCase() === searchValue ? "#c7771d" : "#ccc"
      );
      chart.data.datasets[1].backgroundColor = this.toxinLabels.map(
        (v) => v.toUpperCase() === searchValue ? "#3888e2" : "#ccc"
      );
      }
      else if (!this.checkbox){
        chart.data.datasets[0].backgroundColor = this.toxinLabels.map(
        (v) => v.toUpperCase() === searchValue ? "#c7771d" : "#ccc"
      );
      }
      else if (this.checkbox){
        chart.data.datasets[0].backgroundColor = this.toxinLabels.map(
        (v) => v.toUpperCase() === searchValue ? "#3888e2" : "#ccc"
      );
      }
      
    chart.update();
    },
    convertAndFormat(value) {
      if (value == 0){
        return 'NaN';
      }
      else if (value >= 0.001) {
        return value.toFixed(3) + " mg";
      } else if (value >= 0.000001) {
        return (value * 1000).toFixed(3) + " µg";
      } else {
        return (value * 1000000).toFixed(3) + " ng";
      }
    },
    getToxinContent(selectedOption){
      const index = this.getIndexByToxin(selectedOption)
      const tobaccoValue = this.toxins[index].Tobacco;
      const electronicValue = this.toxins[index].Electronic

      const formattedTobacco = this.convertAndFormat(tobaccoValue);
      const formattedElectronic = this.convertAndFormat(electronicValue);

      if (this.checkboxDisplayBoth){
        
        return `tobacco: ${formattedTobacco}, electronic: ${formattedElectronic}`;
      }
      else if (this.checkbox){
        return `electronic: ${formattedElectronic}`;
      }
      else if (!this.checkbox){
        return `tobacco: ${formattedTobacco}`;
      }
    }
    // async getAllCalulatedUserDataByType(type){
    //   const getURL = "api/TodoApp/GetAllUsersCalculatedDataByType/" + type.toString();
    //   await axios.get(URL_FOR_LOCAL_HOST + getURL)
    //   .then((Response)=>{ this.allUserData = Object.keys(Response.data).map(key => Response.data[key]);
    //     }
    //   )
    // }
  },

  mounted:function(){
        this.getData(),
        this.getOptionsData()
      }
}
</script>

<style>


</style>

