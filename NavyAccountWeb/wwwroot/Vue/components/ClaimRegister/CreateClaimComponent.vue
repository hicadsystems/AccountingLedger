<template>
    <!-- WRAPPER -->
    <div>

        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                <div class="col-xl-6" v-if="wanttoupdate">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>

                </div>

            </div>


        </div>



        <div class="card-body" v-if="dischargecontribution">
            <h3> Contribution </h3>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Description</th>
                        <th>Amount</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="discharges in dischargeList.personContributions">
                        <td>{{ discharges.description}}</td>
                        <td>{{ discharges.amount }}</td>
                    </tr>
                </tbody>

            </table>
        </div>


        <div class="card-body" v-if="dischargeloan">
            <h3> Loan </h3>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Description</th>
                        <th>Amount</th>

                    </tr>
                </thead>
                <tbody>
                    <tr v-for="discharges in dischargeList.personLoans">

                        <td>{{ discharges.description}}</td>
                        <td>{{ discharges.amount }}</td>

                    </tr>
                </tbody>
                <tfoot><tr><td><b>Total Loan</b></td><td><b> {{ this.totalLoan }} </b></td></tr></tfoot>

            </table>
        </div>


        <div class="row card-body col-12" v-if="dischargecontribution || dischargeloan">

            <div class="col-4">
                <div class="form-group">
                    <label class="form-label">Aplication Date</label>
                    <vuejsDatepicker input-class="form-control" name="appdate" v-model="postBody.appdate"></vuejsDatepicker>

                </div>

            </div>

            <div class="col-4">
                

                <div class="form-group" >
                    <br />
                    <button class="btn btn-primary" v-on:click="generateReport" type="button">{{submitorUpdate}}</button>
                </div>
            </div>


        </div>

    </div>


    <!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete';
    import vuejsDatepicker from 'vuejs-datepicker';
    import moment from 'moment';

export default {
     props:['fundcode'],
     components: {
         vuejsDatepicker,
         vuejsAutocomplete
        },
    data() {
        return {
           responseMessage:'',
            errors: null,
            wanttoupdate: true,
            svcnoanddisplay: '',
            searchData: '',
            dischargeList: [],
            dischargecontribution: false,
            dischargeloan : false,
            submitorUpdate: 'Submit',
            autoselectenabled: false,
        canProcess : true,
        postBody: {
                  PersonID: '',
                  appdate:''

        }
        }
        },
    computed: {

            totalLoan() {
                return this.dischargeList.personLoans
                    .map(item => item.amount)
                    .reduce((a, b) => a + b, 0)
            }

        },
        mounted() {
             this.$store.state.objectToUpdate = null,
             this.wanttoupdate = true
     },

    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) {

            this.appdate = this.$store.state.objectToUpdate.appdate;
            this.svcno = this.$store.state.objectToUpdate.PersonID;
              this.wanttoupdate = false;
                this.submitorUpdate = 'Update';this.autoselectenabled = true;

        }
        },

        methods: {
            generateReport() {
                window.open(`/ClaimRegister/finishClaimRequest/${this.format_date(this.postBody.appdate)}/${this.svcnoanddisplay}/${this.fundcode}`, "_blank"); 
            },
            format_date(value){
         if (value) {
           return moment(String(value)).format('YYYYMMDD')
          }
      },
        checkForm: function (e) {

             if (this.postBody.PersonID && this.postBody.appdate) {
             e.preventDefault();
           
              this.canProcess = false;
              this.postPost();
          }
          else{

             this.errors = []
             this.errors.push('Supply the Compulsory fielde(s)')
          }
        },

            setValuePersonID(result) {
            
                this.svcnoanddisplay = result.display.split('_')
                this.svcnoanddisplay = this.svcnoanddisplay[this.svcnoanddisplay.length - 1];
                this.PersonID = result.value
                this.retrieveDischargeCalc()

            },

            retrieveDischargeCalc() {

               axios
                   .get(`/api/ClaimRegister/getDischargeCalculation/${this.svcnoanddisplay}/${this.fundcode}`)
                   .then(response => {

                       this.dischargeList = response.data.data
                       
                       if (response.data.responseCode === "200") {
                           if (this.dischargeList.personContributions) {
                               this.dischargecontribution = true;
                           }

                           if (this.dischargeList.personLoans) {
                               this.dischargeloan = true;
                           }
                       }
                   })
            },
        postPost() {


            if (this.submitorUpdate == 'Submit') {
                  
                    axios.post(`/api/ClaimRegister/CreateClaim`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.PersonID = ''; this.postBody.appdate = '';
                                 //this.postBody.balSheetCode = '';this.postBody.subtype = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
            }

            if (this.submitorUpdate == 'Update') {
                    axios.put(`api/ClaimRegister/updateClaimRegister`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                                this.postBody.PersonID = ''; this.postBody.appdate = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        }
};
</script>