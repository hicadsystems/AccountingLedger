<template>
    <!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/BalanceSheet/CreateFinancial" method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Fund Code</label>
                            <select class="form-control" v-model="postBody.FundCode" name="FundCode" required :readonly="submitorUpdate == 'Update'">
                                <option v-for="balSheet in balanceSheetList" v-bind:value="balSheet.code" v-bind:key="balSheet.code"> {{ balSheet.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Period</label>
                            <input class="form-control" name="Period" v-model="postBody.Period" placeholder="Period" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Interest</label>
                            <input type="text" name="Intrest" class="form-control" v-model="postBody.Interest" placeholder="" />
                        </div>
                    </div>

                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Rate</label>
                            <input class="form-control" name="Rate" v-model="postBody.Rate" placeholder="Rate" />
                        </div>
                        <div class="col-12 ">
                            <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess: true,
            balanceSheetList: null,
            postBody: {
                FundCode: '',
                Period: '',
                Intrest: '',
                Rate: '',
                id:0
            }

        }
        },
      mounted () {
        axios
            .get('/api/FundType/getAllFundTypes')
            .then(response => (this.balanceSheetList = response.data))
      
     },

    watch:{
        '$store.state.objectToUpdate': function (FundCode, Rate, Period, Intrest) {
                this.postBody.FundCode = this.$store.state.objectToUpdate.fundCode,
                this.postBody.Rate = this.$store.state.objectToUpdate.rate,
                this.postBody.Intrest = this.$store.state.objectToUpdate.intrest,
                this.postBody.Period = this.$store.state.objectToUpdate.period
                this.postBody.id = this.$store.state.objectToUpdate.id
                this.submitorUpdate = 'Update';

        }
    },
    methods: {
        checkForm: function (e) {

         if (this.postBody.FundCode) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('FundCode is Required');
          }
        },
        postPost() {

                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/PFfundRate/createPfFunRate`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.FundCode = ''; this.postBody.Rate = '';
                                this.Period = ''; this.Intrest = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            if (this.submitorUpdate == 'Update') { 
                    axios.put(`/api/PFfundRate/updatepFundRate`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                               this.postBody.FundCode = ''; this.postBody.Rate = '';
                                this.Period = ''; this.Intrest = '';
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
                if (objecttoedit.FundCode) {
                    this.postBody.FundCode = objecttoedit.FundCode;
                    this.postBody.Intrest = objecttoedit.Intrest;
                    this.postBody.Rate = objecttoedit.Rate;
                    this.Period = objecttoedit.Period;
                }
            }
        }
};
</script>