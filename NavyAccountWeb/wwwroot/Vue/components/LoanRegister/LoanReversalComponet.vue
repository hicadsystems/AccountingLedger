<template>
    <div>
            <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-success"> {{ responseMessage }}</div>
        <div class="row">
            <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>
                  <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-label">Loan Type</label>

                        <select class="form-control" v-model="postBody.LoanTypeID" name="loanTypeID"  required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.id" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-label">Batch No</label>
              <select class="form-control" v-model="postBody.batchNo" name="batchNo" @change="process">
                  <option v-for="batch in batchlist" v-bind:value="batch.batchNo" v-bind:key="batch.batchNo">{{batch.batchNo}}</option>
              </select>
            </div>
        </div>
         
        <div class="col-md-4">
            <div class="form-group">
                <label class="form-label">Period</label>
              <select class="form-control" v-model="postBody.LoanAppNo" name="period" @change="process2">
                  <option v-for="prd in periodlist" v-bind:value="prd.docno" v-bind:key="prd.docno">{{prd.docno}}</option>
              </select>
            </div>
        </div>
         <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-label">Amount</label>
                        <input class="form-control" name="cramt" v-model="postBody.cramt" placeholder="" readonly/>
                    </div>
                </div>
       
      
         <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Date</label>
                                        <vuejsDatepicker v-model="postBody.ApproveDate" input-class="form-control" name="approveDate"></vuejsDatepicker>
                                    </div>
                                </div>
        </div>
         <div class="col-12 ">
            <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button">{{submitorUpdate}}</button></div>
        </div>
    </div>
</template>
<script>
 import vuejsAutocomplete from 'vuejs-auto-complete'
  import vuejsDatepicker from 'vuejs-datepicker'
export default {
    components:{
        vuejsAutocomplete,
        vuejsDatepicker
    },
  
    data(){
        return{
            responseMessage:'',
            errors:'',
            submitorUpdate : 'Submit',
            searchdata:'',
            batchlist:null,
            periodlist:null,
            perioddata:null,
            loantypeList:null,
            amount:0,

        postBody:{
            PersonID:0,
            LoanTypeID:null,
            batchNo:'',
            ApproveDate:'',
            LoanAppNo:'',
            cramt:0
        }

        };
    },
    mounted() {
            this.$store.state.objectToUpdate = null,
             this.wanttoupdate = true,
        axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
   

         axios
        .get('/Api/LoanRegister/getloanbyBatch2')
        .then(response=>(this.batchlist=response.data))


      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 
         this.postBody.LoanTypeID = this.$store.state.objectToUpdate.loanTypeID,
         this.postBody.ApproveDate = this.$store.state.objectToUpdate.approveDate,
         this.postBody.batchNo = this.$store.state.objectToUpdate.batchNo,
         this.postBody.PersonID = this.$store.state.objectToUpdate.personID

                this.submitorUpdate = 'Update';
               
        }
    },
  
    methods:{
        setValuePersonID(result){
            this.postBody.personID=result.value;
            },
        process(){
           axios
        .get(`/Api/LoanRegister/getloanperiod/${this.postBody.personID}/${this.postBody.LoanTypeID}/${this.postBody.batchNo}`)
        .then(response=>(this.periodlist=response.data
        ))},
        process2(){
           axios
        .get(`/Api/LoanRegister/getloanperiod2/${this.postBody.personID}/${this.postBody.LoanTypeID}/${this.postBody.LoanAppNo}`)
        .then(response=>{this.amount=response.data.amount,
        this.postBody.cramt=this.amount
        })
        },
    checkForm: function (e) {
                    this.postPost();
            },
      postPost() {
       alert('Do you want to Reverse this Loan?');
                axios.put(`/api/LoanRegister/UpdateLoanReversal`, this.postBody)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.submitorUpdate = 'Submit'
                            this.postBody.LoanTypeID = ''; this.postBody.PersonID = '';
                            this.postBody.batchNo = ''; this.postBody.reversalDate = '';
                            this.postBody.LoanAppNo=''; this.postBody.cramt='';

                        }
                    })
                    .catch(e => {
                        this.errors.push(e)
                    });

            }   
 }
 
    
};
</script>