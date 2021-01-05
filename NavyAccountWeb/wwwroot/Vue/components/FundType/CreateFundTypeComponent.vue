<template>
    	<!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/FundType/CreateFundType" method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Code</label>
                            <input type="text" name="Code" class="form-control" v-model="postBody.Code" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Description</label>
                            <input class="form-control" name="Description" v-model="postBody.Description" placeholder="Description" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Start Date</label>

                            <vuejsDatepicker input-class="form-control" name="startDate" v-model="postBody.startDate"> </vuejsDatepicker>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Processing Month</label>
                            <input class="form-control" name="processingMonth" v-model="postBody.processingMonth" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Processing Year</label>
                            <input class="form-control" name="processingYear" v-model="postBody.processingYear" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Income Account</label>
                            <select class="form-control" v-model="postBody.incomeacct" name="incomeacct" required>
                                <option v-for="liability in LiabilityList" v-bind:value="liability.acctcode" v-bind:key="liability.acctcode"> {{ liability.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Interest Account</label>

                            <select class="form-control" v-model="postBody.interestacct" name="interestacct" required>
                                <option v-for="interest in InterestList" v-bind:value="interest.acctcode" v-bind:key="interest.acctcode"> {{ interest.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Fund Account</label>

                            <select class="form-control" v-model="postBody.fundacct" name="fundacct" required>
                                <option v-for="Fund in FundacList" v-bind:value="Fund.acctcode" v-bind:key="Fund.acctcode"> {{ Fund.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">NHQ Trustee Account</label>

                            <select class="form-control" v-model="postBody.trusteeacct" name="trusteeacct" required>
                                <option v-for="Trustee in TrusteeList" v-bind:value="Trustee.acctcode" v-bind:key="Trustee.acctcode"> {{ Trustee.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                    </div>
                </div>
            </div>
        </form>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
     import vuejsDatepicker from 'vuejs-datepicker';
export default {
     components: {
            vuejsDatepicker
        },
    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess: true,
            InterestList: null,
            LiabilityList:null,
            FundacList: null,
            TrusteeList: null,
            postBody: {
                Code: '',
                Description:'',
                startDate: '',
                processingMonth:'',
                processingYear: '',
                incomeacct:'',
                interestacct: '',
                fundacct: '',
                trusteeacct:''

            }
    
        }
        },
     mounted () {
       
         axios
            .get('/api/ChartofAccount/getChartofAccount1/1')
             .then(response => (this.InterestList = response.data))
          
         axios
            .get('/api/ChartofAccount/getChartofAccount5/5')
             .then(response => (this.LiabilityList = response.data))
            axios
            .get('/api/ChartofAccount/getChartofAccount2/2')
                .then(response => (this.TrusteeList = response.data))

          axios
         .get('/api/ChartofAccount/getChartofAccount3/3')
             .then(response => (this.FundacList = response.data))
       
     },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
         this.postBody.Code = this.$store.state.objectToUpdate.code,
         this.postBody.Description = this.$store.state.objectToUpdate.description,
         this.postBody.startDate = this.$store.state.objectToUpdate.startDate,
         this.postBody.processingMonth = this.$store.state.objectToUpdate.processingMonth,
             this.postBody.processingYear = this.$store.state.objectToUpdate.processingYear,
             this.postBody.fundacct = this.$store.state.objectToUpdate.fundacct,
             this.postBody.incomeacct = this.$store.state.objectToUpdate.incomeacct,
             this.postBody.interestacct = this.$store.state.objectToUpdate.interestacct,
             this.postBody.trusteeacct = this.$store.state.objectToUpdate.trusteeacct
         this.submitorUpdate = 'Update';
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.Code) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Code is Required');
          }
        },
        postPost() {
                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/FundType/createFundType`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.Code = ''; this.postBody.Code = '';
                                this.postBody.Description = ''; this.postBody.Description = '';
                                this.postBody.startDate = '';this.postBody.startDate = '';
                                this.postBody.processingMonth = '';this.postBody.processingMonth = '';
                                this.postBody.processingYear = ''; this.postBody.processingYear = '';
                                 this.postBody.fundacct = ''; this.postBody.fundacct = '';
                                this.postBody.incomeacct = ''; this.postBody.incomeacct = '';
                                this.postBody.trusteeacct = ''; this.postBody.trusteeacct = '';
                                this.postBody.interestacct = ''; this.postBody.interestacct = '';
                                 this.$store.state.objectToUpdate = "create";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                if(this.submitorUpdate == 'Update'){
                    axios.put(`/api/FundType/updateFundType`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.submitorUpdate = 'Submit'
                                this.postBody.Code = '';this.postBody.Code = '';
                                this.postBody.Description = '';this.postBody.Description = '';
                                this.postBody.startDate = '';this.postBody.startDate = '';
                                this.postBody.processingMonth = '';this.postBody.processingMonth = '';
                                this.postBody.processingYear = ''; this.postBody.processingYear = '';
                                this.postBody.fundacct = ''; this.postBody.fundacct = '';
                                this.postBody.incomeacct = ''; this.postBody.incomeacct = '';
                                this.postBody.trusteeacct = ''; this.postBody.trusteeacct = '';
                                this.postBody.interestacct = ''; this.postBody.interestacct = '';
                                 this.$store.state.objectToUpdate = "update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;
                if(objecttoedit.Code){
                    this.postBody.Code = objecttoedit.Code;
                    this.postBody.Description = objecttoedit.Description;
                    this.postBody.startDate = objecttoedit.startDate;
                    this.postBody.processingMonth = objecttoedit.processingMonth;
                    this.postBody.processingYear = objecttoedit.processingYear;
                     this.postBody.fundacct = objecttoedit.fundacct,
                     this.postBody.incomeacct = objecttoedit.incomeacct,
                     this.postBody.interestacct = objecttoedit.interestacct,
                     this.postBody.trusteeacct = objecttoedit.trusteeacct
                  
                }
            }
        }
};
</script>